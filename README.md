# fiap-fcg-game-api

**Microsserviço de Games** do ecossistema FIAP Cloud Games. Responsável pelo gerenciamento de jogos, promoções e notificações, esse serviço opera de forma isolada e integrada aos demais serviços via APIs REST e gRPC, seguindo princípios de microsserviços e implantação em nuvem (Azure).

## TechChallenge CloudGames

API REST independente e escalável, desenvolvida como parte do Tech Challenge da FIAP. Essa aplicação foi extraída de um monolito e redesenhada como **microsserviço**, com base em Clean Architecture, uso de Entity Framework Core com PostgreSQL e integração com serviços externos via gRPC.

## Sobre o Projeto

O objetivo é fornecer um microsserviço resiliente e coeso para o gerenciamento de jogos, promoções e sistema de notificações da plataforma FIAP Cloud Games. Ele pode ser implantado e escalado de forma autônoma, cumprindo um papel específico dentro da arquitetura distribuída da aplicação.

### Arquitetura

O microsserviço segue os princípios da **Clean Architecture**, promovendo separação de responsabilidades, testabilidade e independência entre camadas:

- `WebApi`: camada HTTP com controle de rotas, autenticação e Swagger.
- `Application`: coordena casos de uso e regras de negócio.
- `Domain`: regras de domínio, entidades e validações.
- `Infrastructure`: implementações de repositórios com EF Core e integrações (ex: gRPC, HTTP clients).

### Pré-requisitos

| Requisito        | Link para Download   |
| ---------------- |----------------------|
| `.NET SDK 8.0`   | [Baixar aqui](https://dotnet.microsoft.com/en-us/download)   |
| `PostgreSQL 14+` | [Baixar aqui](https://www.postgresql.org/download/)   |
| `Docker`         | [Baixar aqui](https://www.docker.com/products/docker-desktop/)   |
| `Docker Compose` | [Baixar aqui](https://docs.docker.com/compose/install/)   |

## Execute localmente

<details><summary>1. Clone o repositório</summary>
  
```bash
git clone https://github.com/seu-usuario/fiap-fcg-game-api.git
cd fiap-fcg-game-api
```

</details> 

<details><summary>2. Suba o banco de dados com Docker Compose</summary>
  
```bash
docker-compose up -d
```
Certifique-se de usar a porta certa, por exemplo: 5433.

</details> 

<details><summary>3. Configure as variáveis de ambiente</summary>
  
```json
"GAME_CONNECTION_STRING": "Host=localhost;Port=5433;Database=fcg_games_db;Username=postgres;Password=postgres",
"JWT_KEY": "chave-super-secreta-para-assinar-jwt",
"JWT_ISSUER": "Fiap.FCG",
"JWT_AUDIENCE": "Fiap.FCG",
"URI_USUARIO_API": "http://localhost:8181",
"SERVICEBUS_CONNECTION": "Endpoint=sb://fiap-games-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=...",
"ENVIA_NOTIFICACAO_INTERVALO_MINUTOS": 10
```

</details> 

<details><summary>4. Aplique as migrations</summary>
  
```bash
dotnet ef database update \
  --project src/Fiap.FCG.Game.Infrastructure \
  --startup-project src/Fiap.FCG.Game.WebApi
```

</details> 

<details><summary>5. Execute a aplicação</summary>
  
```bash
dotnet run --project src/Fiap.FCG.Game.WebApi
```

Acesse: [https://localhost:5000/swagger](https://localhost:5000/swagger)
</details>

## Tecnologias Utilizadas
| Tecnologia            | Link                                                                                                                                                   |
| --------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------ |
| .NET 8                | [https://learn.microsoft.com/en-us/dotnet/](https://learn.microsoft.com/en-us/dotnet/)                                                                 |
| Entity Framework Core | [https://learn.microsoft.com/en-us/ef/core/](https://learn.microsoft.com/en-us/ef/core/)                                                               |
| PostgreSQL            | [https://www.postgresql.org/docs/](https://www.postgresql.org/docs/)                                                                                   |
| Swashbuckle (Swagger) | [https://github.com/domaindrivendev/Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)                                 |
| MediatR               | [https://github.com/jbogard/MediatR](https://github.com/jbogard/MediatR)                                                                               |
| gRPC                  | [https://grpc.io/docs/languages/csharp/](https://grpc.io/docs/languages/csharp/)                                                                       |
| Azure Service Bus     | [https://learn.microsoft.com/en-us/azure/service-bus-messaging/](https://learn.microsoft.com/en-us/azure/service-bus-messaging/)                     |

### Variáveis de Ambiente

| Variável                    | Descrição                              | Obrigatório | Exemplo                       |
| --------------------------- | -------------------------------------- | ----------- | ----------------------------- |
| `GAME_CONNECTION_STRING`    | String de conexão com PostgreSQL      | Sim         | `"Host=localhost;Port=5433;Database=fcg_games_db;Username=postgres;Password=postgres"` |
| `JWT_KEY`                   | Chave secreta para assinar o token JWT | Sim         | `"minha-chave-super-secreta"` |
| `JWT_ISSUER`                | Emissor do token JWT                   | Sim         | `"Fiap.FCG"`                  |
| `JWT_AUDIENCE`              | Público-alvo do token JWT              | Sim         | `"Fiap.FCG"`                  |
| `URI_USUARIO_API`               | URL do microsserviço de usuários | Sim         | `"http://localhost:8181"`      |
| `SERVICEBUS_CONNECTION`         | Connection string do Azure Service Bus | Sim | `"Endpoint=sb://fiap-games-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=..."` |
| `ENVIA_NOTIFICACAO_INTERVALO_MINUTOS` | Intervalo em minutos para envio de notificações | Não | `10` |

## Funcionalidades

### Jogos
- Cadastro, consulta e atualização de jogos
- Gerenciamento de catálogo de jogos

### Promoções
- Criação e gerenciamento de promoções
- Associação de jogos às promoções
- Controle de período de validade

### Notificações
- Sistema de envio de notificações por email
- Integração com serviço de usuários para obter lista de usuários
- Controle de usuários já notificados para evitar spam
- Processamento assíncrono via Azure Service Bus

## Integrações

### Serviço de Usuários
O microsserviço se integra com o serviço de usuários via HTTP para:
- Obter lista de usuários que recebem notificações
- Endpoint: `GET /usuarios/notificacoes`

### Azure Service Bus
Utilizado para processamento assíncrono de mensagens:
- Envio de notificações em background
- Processamento de eventos de promoções
- Garantia de entrega e retry automático