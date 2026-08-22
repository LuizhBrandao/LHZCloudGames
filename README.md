# LHZ Cloud Games - Tech Challenge Fase 1

Bem-vindo ao repositório do **LHZ Cloud Games**, o projeto desenvolvido para a Fase 1 do Tech Challenge.

## 🎯 Objetivos do Projeto
O objetivo desta fase é criar uma API REST em **.NET 8** para gerenciar usuários e uma biblioteca de jogos adquiridos. O projeto serve como um MVP (Minimum Viable Product) de uma plataforma de venda de jogos digitais, garantindo a persistência de dados, qualidade de software e boas práticas de desenvolvimento (com testes automatizados e modelagem tática do DDD).

## 🚀 Tecnologias Utilizadas
- **.NET 8** (Minimal APIs)
- **Entity Framework Core**
- **Banco de Dados**: PostgreSQL
- **Autenticação**: JWT (JSON Web Token)
- **Testes Unitários**: xUnit + FluentAssertions + Moq
- **Arquitetura**: Monolito estruturado sob os princípios de Domain-Driven Design (DDD)

## ⚙️ Estrutura da Arquitetura
O projeto foi dividido em camadas (DDD):
- **Domain**: Entidades de negócio (`User`, `Game`, `UserGame`), Regras de validação (senha forte e e-mail) e Interfaces de repositório.
- **Application**: Casos de uso (`AuthService`, `GameService`) e DTOs de entrada e saída.
- **Infrastructure**: Integração de banco de dados (`ApplicationDbContext`), Migrations, implementações de repositórios e serviços de infraestrutura (Token JWT).
- **Api**: Configuração do Swagger, Middleware global para tratamento de exceções e endpoints (Minimal APIs).
- **Tests**: Testes focados nas validações e regras de negócio (TDD/BDD).

## 📋 Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/) rodando localmente (ou via Docker).

## 🛠️ Instruções de Uso e Execução

### 1. Clonar o repositório:
```bash
git clone https://github.com/LuizhBrandao/LHZCloudGames.git
cd LHZCloudGames
```

### 2. Configurar o Banco de Dados:
Certifique-se de que o PostgreSQL está rodando. A string de conexão padrão no arquivo `LHZCloudGames/LHZCloudGames.Api/appsettings.json` é:
```json
"Host=localhost;Database=LHZCloudGames;Username=postgres;Password=postgres"
```
*(Altere as credenciais caso o seu ambiente local exija senhas ou portas diferentes).*

### 3. Aplicar as Migrations:
A partir da raiz do repositório, você precisa gerar o banco de dados rodando os comandos do Entity Framework. 
Navegue até a pasta do projeto de inicialização (API) e execute o update do banco:
```bash
cd LHZCloudGames/LHZCloudGames.Api
dotnet ef database update --project ../LHZCloudGames.Infrastructure
```
*(Se você não tiver as ferramentas do EF instaladas, rode `dotnet tool install --global dotnet-ef` antes).*

### 4. Rodar a Aplicação:
Volte para a pasta raiz da solução ou rode a API de dentro de seu diretório:
```bash
dotnet run
```

### 5. Testar os Endpoints (Swagger):
Ao rodar, a API subirá em portas do `localhost` (ex: `http://localhost:5000`).
Abra seu navegador e acesse a documentação do Swagger:
- `http://localhost:5000/swagger` (Verifique a porta exata exibida no console após o comando `dotnet run`).

## 🧪 Como rodar os Testes Automatizados
Para executar a suíte de testes unitários que garantem o funcionamento correto das regras de negócio (ex: validação de senhas com critérios de segurança):
```bash
cd LHZCloudGames/LHZCloudGames.Tests
dotnet test
```
