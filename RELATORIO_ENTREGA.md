# Relatório de Entrega - Tech Challenge Fase 2
**FIAP Pós Tech - Software Architecture / Cloud Games**

---

## 1. Identificação do Grupo e Integrantes

* **Nome do Grupo / Projeto**: LHZ Cloud Games (FCG)
* **Participantes e Discord**:
  * **Nome**: Luiz Henrique Oliveira Brandão | **Discord**: `luizhbrandao` (ou preencher com o username exato do Discord)
  * *(Adicionar outros membros caso aplicável)*

---

## 2. Links Principais de Entrega

* **Link do Repositório (GitHub)**: [https://github.com/LuizhBrandao/LHZCloudGames](https://github.com/LuizhBrandao/LHZCloudGames)
* **Link da Documentação Técnica (README)**: [https://github.com/LuizhBrandao/LHZCloudGames/blob/main/README.md](https://github.com/LuizhBrandao/LHZCloudGames/blob/main/README.md)
* **Link do Vídeo Demonstrativo (YouTube)**: `[INSERIR O LINK DO VÍDEO AQUI]` (Vídeo não listado ou público de até 15 minutos)

---

## 3. Checklist dos Requisitos Atendidos

| Requisito | Status | Descrição da Implementação |
| :--- | :---: | :--- |
| **Arquitetura Monolito** | ✅ Concluído | Monolito desacoplado em camadas DDD (.NET 10 Minimal APIs), stateless e escalável horizontalmente. |
| **Dockerização Otimizada** | ✅ Concluído | `Dockerfile` multi-stage build gerando imagem leve (~99MB) baseada em runtime ASP.NET 10 com usuário não-root. |
| **Stack de Monitoramento** | ✅ Concluído | Métricas nativas via `prometheus-net.AspNetCore` (`/metrics`), `/health`, integradas com Prometheus e Grafana (dashboard completo provisionado). |
| **Pipeline de CI (Testes)** | ✅ Concluído | GitHub Actions executado na abertura de PR/Commit (`.github/workflows/ci.yml`), realizando restore, build, testes unitários e validação do Dockerfile. |
| **Pipeline de CD (Deploy)** | ✅ Concluído | GitHub Actions executado no merge na branch `main` (`.github/workflows/cd.yml`), executando testes, build da imagem Docker, publicação em Container Registry e deploy na cloud. |
| **Pipeline Azure DevOps** | ✅ Concluído | Arquivo `azure-pipelines.yml` estruturado em estágios de CI e CD para execução no Azure DevOps. |
| **Publicação na Cloud** | ✅ Concluído | Suporte e guia documentado para deploy em contêineres na AWS (App Runner / ECS), Azure (Container Apps / App Service) ou Render. |
| **README Completo** | ✅ Concluído | Instruções detalhadas de arquitetura, execução local com Docker Compose, visualização de métricas e CI/CD. |

---

## 4. Roteiro Sugerido para Gravação do Vídeo (Até 15 Minutos)

Para obter a nota máxima e cobrir todos os critérios avaliados pela banca:

### Parte 1: Introdução e Arquitetura (0:00 - 2:30)
- Apresentar os membros do grupo.
- Explicar brevemente a evolução do projeto da Fase 1 para a Fase 2:
  - Foco em escalabilidade, resiliência, automação e observabilidade.
  - Manutenção do monolito para agilidade e implementação robusta em nuvem.

### Parte 2: Dockerização e Execução Local (2:30 - 5:30)
- Explicar o `Dockerfile` multi-stage:
  - Separação da etapa de compilação (SDK) da etapa de execução (ASP.NET Runtime enxuto).
  - Otimização de camadas de cache e imagem com menos de 100MB.
- Demonstrar o `docker compose up -d`:
  - Mostrar os contêineres subindo: API, PostgreSQL, Prometheus e Grafana.
  - Demonstrar as migrations automáticas sendo executadas pelo contêiner da API.
  - Acessar o Swagger em `http://localhost:8080/swagger` e testar um endpoint (ex.: `/health` ou cadastro de usuário).

### Parte 3: Stack de Monitoramento em Ação (5:30 - 9:30)
- Demonstrar o endpoint `/metrics` da aplicação:
  - Explicar as métricas de tempo de resposta, contagem de requisições por status code e memória do .NET.
- Acessar o **Prometheus** em `http://localhost:9090`:
  - Mostrar o target `fcg-api` em estado `UP`.
  - Executar uma query rápida (ex.: `http_requests_received_total`).
- Acessar o **Grafana** em `http://localhost:3000`:
  - Mostrar o dashboard pré-configurado `FCG - FIAP Cloud Games Metrics`.
  - Executar algumas requisições no Swagger/Postman e mostrar os gráficos de Throughput (RPS), Latência (p50/p90/p99) e Status Code HTTP sendo atualizados em tempo real.

### Parte 4: CI/CD no Repositório (9:30 - 13:00)
- Abrir a aba **Actions** no GitHub:
  - Mostrar o pipeline de **CI** (`ci.yml`) executado com sucesso com os "checks verdes" nos testes automatizados.
  - Mostrar o pipeline de **CD** (`cd.yml`) executado após merge na branch `main`:
    - Etapa de Quality Gate (testes).
    - Etapa de Build e Push da imagem Docker para o Container Registry.
    - Etapa de Deploy na Cloud.

### Parte 5: Demonstração na Cloud e Encerramento (13:00 - 15:00)
- Mostrar a aplicação rodando no ambiente de Cloud escolhido (URL pública acessível).
- Fazer uma requisição rápida para demonstrar disponibilidade e resiliência na nuvem.
- Considerações finais e encerramento.
