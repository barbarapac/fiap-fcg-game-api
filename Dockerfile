# build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore ./src/Fiap.FCG.Game.WebApi/Fiap.FCG.Game.WebApi.csproj
RUN dotnet publish ./src/Fiap.FCG.Game.WebApi/Fiap.FCG.Game.WebApi.csproj -c Release -o /app/publish /p:UseAppHost=false

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Fiap.FCG.Game.WebApi.dll"]
