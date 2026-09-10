#  LHZ Cloud Games (FCG) - Tech Challenge Fase 2

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat&logo=dotnet)
![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?style=flat&logo=docker)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-4169E1?style=flat&logo=postgresql)
![Prometheus](https://img.shields.io/badge/Prometheus-Monitoring-E6522C?style=flat&logo=prometheus)
![Grafana](https://img.shields.io/badge/Grafana-Dashboards-F46800?style=flat&logo=grafana)
![CI/CD](https://img.shields.io/badge/CI%2FCD-GitHub%20Actions-2088FF?style=flat&logo=githubactions)

Bem-vindo ao repositório do **LHZ Cloud Games**, projeto desenvolvido por Luiz Henrique Oliveira Brandão para a Pós Tech FIAP.

A **Fase 2** tem como objetivo transformar o MVP da plataforma em um ecossistema **escalável, resiliente, conteinerizado e monitorável**, com deploys automatizados via pipelines de **CI/CD** e observabilidade em tempo real com **Prometheus** e **Grafana**.

---

##  Objetivos da Fase 2

1. **Dockerização Otimizada**: Criação de `Dockerfile` multi-stage gerando uma imagem de produção enxuta (~99MB) executando com usuário não-root.
2. **Orquestração Completa**: `docker-compose.yml` integrando a API, o banco de dados PostgreSQL com persistência de volume e a stack de observabilidade.
3. **Observabilidade & Monitoramento**:
   - Métricas de aplicação e runtime expostas em `/metrics` via `prometheus-net.AspNetCore`.
   - Endpoint de saúde em `/health`.
   - Coletor Prometheus configurado com scrape a cada 5 segundos.
   - Dashboard do Grafana pré-provisionado com Throughput (RPS), Latência (p50/p90/p99), Status Codes HTTP e consumo de memória/CPU.
4. **Automação de CI/CD**:
   - **CI (`ci.yml`)**: Disparo automático na abertura de Pull Requests e Commits para validação de compilação, execução de testes unitários e teste de build do Docker.
   - **CD (`cd.yml`)**: Disparo no merge da branch `main`, executando quality gate, build e push da imagem para Container Registry (GHCR / Docker Hub) e deploy na Cloud.
   - Pipeline alternativa para **Azure DevOps** (`azure-pipelines.yml`).
5. **Resiliência de Banco de Dados**: Aplicação automática de migrações do Entity Framework Core na inicialização do contêiner com política de retentativas.

---

##  Arquitetura do Sistema

Seguindo as diretrizes da fase, o sistema é estruturado como um **monolito modular** orientado a **Domain-Driven Design (DDD)**, stateless e preparado para escalabilidade horizontal na nuvem:

```
├── .github/workflows/
│   ├── ci.yml                 # Pipeline de Integração Contínua (Testes e Validação)
│   └── cd.yml                 # Pipeline de Entrega Contínua (Build, Push e Deploy)
├── monitoring/
│   ├── prometheus/
│   │   └── prometheus.yml     # Configuração de scraping do Prometheus
│   └── grafana/
│       ├── provisioning/      # Provisionamento automático de DataSources e Dashboards
│       └── dashboards/
│           └── fcg-dashboard.json # Dashboard de métricas da aplicação
├── LHZCloudGames/
│   ├── LHZCloudGames.Domain/         # Entidades, Enums, Interfaces de Repositório
│   ├── LHZCloudGames.Application/    # Casos de Uso, Serviços e DTOs
│   ├── LHZCloudGames.Infrastructure/ # EF Core DbContext, Migrations, Repositórios, JWT
│   ├── LHZCloudGames.Api/            # Minimal APIs, Middlewares, Endpoints, Métricas
│   └── LHZCloudGames.Tests/          # Testes Unitários com xUnit e FluentAssertions
├── Dockerfile                 # Multi-stage build otimizado para .NET 10
├── docker-compose.yml         # Orquestração local (API + Postgres + Prometheus + Grafana)
├── azure-pipelines.yml        # Pipeline para Azure DevOps
└── RELATORIO_ENTREGA.md       # Documento de entrega para avaliação da FIAP
```

---

##  Como Executar Localmente

### Pré-requisitos
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) instalado e em execução.

### Subindo todo o ecossistema com Docker Compose:
Basta clonar o repositório e executar na raiz:

```bash
git clone https://github.com/LuizhBrandao/LHZCloudGames.git
cd LHZCloudGames
docker compose up -d --build
```

Em poucos segundos, todos os 4 serviços estarão operacionais:
- **API (.NET 10)**: [http://localhost:8080](http://localhost:8080)
- **Documentação Swagger**: [http://localhost:8080/swagger](http://localhost:8080/swagger)
- **Health Check**: [http://localhost:8080/health](http://localhost:8080/health)
- **Métricas Prometheus**: [http://localhost:8080/metrics](http://localhost:8080/metrics)
- **Prometheus Server**: [http://localhost:9090](http://localhost:9090)
- **Grafana**: [http://localhost:3000](http://localhost:3000) *(Usuário: `admin` | Senha: `admin`)*

---

##  Observabilidade & Monitoramento

A stack de monitoramento foi configurada para validar a saúde e performance da aplicação sob tráfego:

### 1. Prometheus ([http://localhost:9090](http://localhost:9090))
- O Prometheus realiza scrape das métricas da API a cada 5 segundos no endpoint `http://fcg-api:8080/metrics`.
- No menu **Status -> Targets**, o endpoint `fcg-api` estará em estado `UP`.
- Principais métricas monitoradas:
  - `http_requests_received_total`: Total de requisições por endpoint, método e status code.
  - `http_request_duration_seconds`: Histograma de latência das requisições.
  - `http_requests_in_progress`: Requisições em processamento concorrente.
  - `process_working_set_bytes`: Consumo de memória da aplicação.
  - `process_cpu_seconds_total`: Uso de CPU do processo.

### 2. Grafana ([http://localhost:3000](http://localhost:3000))
- O Grafana já inicializa com o DataSource Prometheus provisionado e o painel **FCG - FIAP Cloud Games Metrics** carregado automaticamente.
- Para acessar o painel:
  1. Acesse `http://localhost:3000` (login: `admin` / `admin`).
  2. Vá em **Dashboards** e selecione **FCG - FIAP Cloud Games Metrics**.
  3. Visualize os gráficos de Throughput (RPS), Latência p50/p90/p99, códigos de resposta HTTP (2xx, 4xx, 5xx) e consumo de recursos.

---

##  Pipelines de CI/CD (GitHub Actions)

### 1. Integração Contínua (CI - `ci.yml`)
- **Gatilhos**: Abertura de Pull Requests para `main` e pushes em branches secundárias.
- **Etapas**:
  1. Checkout do código-fonte.
  2. Instalação e configuração do .NET SDK 10.
  3. Restauração de dependências (`dotnet restore`).
  4. Compilação da solução em modo Release (`dotnet build`).
  5. Execução dos testes automatizados (`dotnet test`) com geração de relatório TRX.
  6. Validação do build do Dockerfile (`docker build`) para prevenir erros de conteinerização antes do merge.

### 2. Entrega Contínua (CD - `cd.yml`)
- **Gatilhos**: Push/Merge aprovado na branch principal `main`.
- **Etapas**:
  1. **Quality Gate**: Reexecução da suíte de testes.
  2. **Build & Push**: Compilação da imagem Docker multi-stage e publicação no **GitHub Container Registry (GHCR)** com tags `latest` e `${{ github.sha }}` (com suporte a Docker Hub via secrets).
  3. **Deploy**: Atualização do contêiner no provedor de nuvem (via Webhook ou CLI da cloud).

---

##  Publicação na Cloud

A aplicação é 100% stateless e utiliza variáveis de ambiente padrão do ASP.NET Core, permitindo publicação em qualquer provedor de nuvem:

### Opção 1: AWS (App Runner / ECS Fargate)
1. Crie uma instância de PostgreSQL no **AWS RDS** (ou utilize um banco gerenciado gratuito como Neon/Supabase).
2. Faça o deploy da imagem Docker diretamente no **AWS App Runner** apontando para o repositório de contêineres:
   - Configure a porta `8080`.
   - Defina a variável `ConnectionStrings__DefaultConnection` com os dados do RDS.
   - Defina `Jwt__Key` com uma chave segura.
   - O App Runner provisiona automaticamente certificado HTTPS e auto-scaling.

### Opção 2: Azure (Container Apps / App Service)
1. Crie um **Azure Database for PostgreSQL**.
2. No **Azure Container Apps**, configure um container apontando para a imagem publicada no GHCR/ACR.
3. Configure as variáveis de ambiente equivalentes e habilite o Ingress HTTP na porta `8080`.

### Opção 3: Provedores PaaS com Camada Gratuita (Render / Railway)
1. Suba o PostgreSQL gerenciado gratuito.
2. Aponte o serviço Web para o repositório ou imagem Docker, configurando `ConnectionStrings__DefaultConnection` com a URL do banco.

---

##  Como Rodar os Testes Automatizados

Para executar os testes unitários localmente via linha de comando:

```bash
dotnet test LHZCloudGames/LHZCloudGames.slnx
```

---

##  Entregáveis da Fase 2

Conforme os critérios de avaliação:
- **Código-fonte**: Completo neste repositório.
- **Relatório de Entrega**: Disponível em [RELATORIO_ENTREGA.md](RELATORIO_ENTREGA.md) e [relatorio_entrega.txt](relatorio_entrega.txt).
- **Roteiro do Vídeo**: Instruções detalhadas para a gravação da apresentação de até 15 minutos disponíveis no relatório de entrega.
