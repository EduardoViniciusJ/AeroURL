# AeroURL

API de encurtamento de URLs em .NET 10, construída do zero como estudo de system design, performance, arquitetura e mensageria.

## Status
**Planejamento concluído; implementação não iniciada.** O andamento está em [TASKS.md](TASKS.md). Os comandos abaixo passam a funcionar conforme as tarefas T01 e T02 forem concluídas.

## Funcionalidades
- Login com Google ou GitHub (OAuth).
- Encurtar uma URL: ID atômico (Redis `INCR`), embaralhado e codificado em Base62.
- Redirecionar (HTTP 307) com cache-aside no Redis e fallback no PostgreSQL.
- Contar cliques de forma assíncrona com RabbitMQ e um Worker.
- Consultar o total de cliques de um link próprio.

## Stack
| Camada | Tecnologia |
|---|---|
| API | .NET 10, ASP.NET Core Minimal APIs |
| Banco | PostgreSQL com EF Core |
| Cache e IDs | Redis |
| Mensageria | RabbitMQ |
| Load balancer | NGINX |
| Testes | xUnit, Testcontainers, BenchmarkDotNet |
| CI | GitHub Actions |

## Pré-requisitos
- .NET SDK 10
- Docker com Docker Compose
- Credenciais OAuth de um app no Google e de um no GitHub (para o login)

## Configuração
Copie `.env.example` para `.env` e preencha. Nunca commite o `.env`. Os nomes finais das variáveis são definidos nas tarefas T02 e T16; a lista esperada é:

| Variável | Para quê |
|---|---|
| Usuário, senha e nome do banco PostgreSQL | Persistência |
| Endereço do Redis | Cache e geração de ID |
| Endereço, usuário e senha do RabbitMQ | Eventos de clique |
| Client ID e client secret do Google | Login |
| Client ID e client secret do GitHub | Login |

## Como executar
```bash
docker compose up -d                          # PostgreSQL, Redis e RabbitMQ
dotnet run --project backend/src/AeroUrl.Api
dotnet run --project backend/src/AeroUrl.Worker
```

## Como testar
```bash
dotnet test backend
```
Os testes de integração sobem PostgreSQL, Redis e RabbitMQ reais com Testcontainers, então o Docker precisa estar rodando.

## Estrutura de pastas
```
AeroURL/
├── docker-compose.yml
├── backend/
│   ├── src/
│   │   ├── AeroUrl.Core/            regras, entidades, interfaces, Base62
│   │   ├── AeroUrl.Infrastructure/  PostgreSQL, Redis, RabbitMQ
│   │   ├── AeroUrl.Api/             endpoints por feature
│   │   └── AeroUrl.Worker/          consumidor de eventos de clique
│   └── tests/
│       ├── AeroUrl.UnitTests/
│       └── AeroUrl.IntegrationTests/
└── frontend/                        fora do escopo atual
```

## Documentação
| Documento | Conteúdo |
|---|---|
| [BRIEF.md](BRIEF.md) | Resumo de 1 página |
| [PRD.md](PRD.md) | Requisitos, arquitetura e modelo de dados |
| [SCOPE.md](SCOPE.md) | Escopo, decisões e hipóteses |
| [TASKS.md](TASKS.md) | Plano de execução e registro de validações |
| [AGENTS.md](AGENTS.md) | Regras para agentes de IA |

## Licença
Não definida.
