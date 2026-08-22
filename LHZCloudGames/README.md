# LHZ Cloud Games API

API REST em .NET 8 para gerenciamento de usuários e jogos, construída para a fase 1 do Tech Challenge (FIAP Cloud Games).

## Tecnologias
- .NET 8 (Minimal APIs)
- Entity Framework Core (PostgreSQL)
- JWT Authentication
- Swagger
- xUnit & FluentAssertions para testes unitários

## Estrutura do Projeto (DDD)
- **Domain**: Entidades (User, Game, UserGame), validações e interfaces.
- **Application**: Casos de uso (AuthService, GameService) e DTOs.
- **Infrastructure**: Implementação do EF Core e gerador de Token JWT.
- **Api**: Minimal APIs, Middleware de Erros e Swagger.
- **Tests**: Testes unitários para regras de negócio e validação de senhas.

## Como Executar
1. Certifique-se de ter o PostgreSQL rodando localmente (ou ajuste a connection string em ppsettings.json).
2. Rode dotnet ef database update na pasta da API para criar as tabelas.
3. Rode dotnet run --project LHZCloudGames.Api para iniciar o projeto.
4. Acesse http://localhost:<porta>/swagger para ver a documentação da API.
