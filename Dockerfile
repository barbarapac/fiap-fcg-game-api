# ============================================
# Stage 1: Build (SDK Debian - para compilar gRPC)
# ============================================
# Usa Debian porque o gRPC precisa do compilador protoc (requer glibc)
FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
WORKDIR /src

# 0. Copiar arquivos de configuração centralizados
# Esses arquivos definem TargetFramework, versões de pacotes e outras propriedades globais
# Precisam estar disponíveis ANTES do restore para evitar erro NETSDK1013
COPY ["Directory.Build.props", "."]
COPY ["Directory.Packages.props", "."]

# 1. Copiar apenas arquivos .csproj dos projetos
# Estratégia de cache: se apenas código .cs mudar, essa layer é reutilizada
# Resultado: restore de pacotes NuGet não é refeito desnecessariamente
COPY ["src/Fiap.FCG.Game.Application/Fiap.FCG.Game.Application.csproj", "src/Fiap.FCG.Game.Application/"]
COPY ["src/Fiap.FCG.Game.Domain/Fiap.FCG.Game.Domain.csproj", "src/Fiap.FCG.Game.Domain/"]
COPY ["src/Fiap.FCG.Game.Infrastructure/Fiap.FCG.Game.Infrastructure.csproj", "src/Fiap.FCG.Game.Infrastructure/"]
COPY ["src/Fiap.FCG.Game.WebApi/Fiap.FCG.Game.WebApi.csproj", "src/Fiap.FCG.Game.WebApi/"]

# 2. Restore de pacotes NuGet
# Baixa todas as dependências definidas nos .csproj e Directory.Packages.props
# Esta layer é cacheada e só é refeita se os arquivos .csproj ou props mudarem
RUN dotnet restore "src/Fiap.FCG.Game.WebApi/Fiap.FCG.Game.WebApi.csproj"

# 3. Copiar todo o código-fonte restante
# Feito APÓS o restore para maximizar reuso de cache
# Se apenas código mudar, layers anteriores (restore) permanecem em cache
COPY . .

# 4. Publish da aplicação
# -c Release: compila em modo otimizado para produção
# -o /app/publish: define diretório de saída dos binários
# /p:UseAppHost=false: não gera executável nativo (reduz tamanho)
# /p:PublishTrimmed=false: desabilita remoção de código não usado (compatibilidade com MediatR/reflection)
# Não usa --no-restore para garantir que todos os pacotes (incluindo analyzers) estejam disponíveis
RUN dotnet publish "src/Fiap.FCG.Game.WebApi/Fiap.FCG.Game.WebApi.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false \
    /p:PublishTrimmed=false

# ============================================
# Stage 2: Runtime (ASP.NET Alpine - super leve)
# ============================================
# Usa Alpine (distribuição Linux minimalista) para runtime
# Imagem base: ~60 MB (vs ~220 MB do Debian)
# Não precisa de SDK, ferramentas de build ou protoc - apenas runtime .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final

# Criar usuário não-root (segurança)
RUN addgroup -g 1000 appgroup && \
    adduser -u 1000 -G appgroup -s /bin/sh -D appuser

WORKDIR /app

# Copiar APENAS os binários publicados do stage anterior
# Não copia SDK, código-fonte, arquivos intermediários ou ferramentas de build
# Resultado: imagem final contém apenas o necessário para executar a aplicação
COPY --from=build /app/publish .

# Ajustar permissões
RUN chown -R appuser:appgroup /app

# Trocar para usuário não-root
USER appuser

# Define a URL que a aplicação ASP.NET vai escutar
# Usa porta 8080 (padrão para containers não-root)
ENV ASPNETCORE_URLS=http://+:8080

# Expõe a porta 8080 para acesso externo
EXPOSE 8080

# Define o comando de inicialização do container
# Executa a DLL da aplicação usando o runtime .NET
ENTRYPOINT ["dotnet", "Fiap.FCG.Game.WebApi.dll"]
