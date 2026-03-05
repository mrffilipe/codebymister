# CodeByMister Backend

Backend em **.NET 8** seguindo Clean Architecture com padrão CQRS.

## Estrutura

```text
backend/
  Codebymister.Domain/         # Entidades, enums, contratos de repositório
  Codebymister.Application/    # Use cases (Commands/Queries), DTOs, interfaces
  Codebymister.Infrastructure/ # EF Core, repositórios, queries, serviços, configurações
    └── Persistence/
        ├── Repositories/      # Implementações de repositórios (escrita)
        └── Queries/           # Queries otimizadas (leitura com projeção direta para DTOs)
  Codebymister.API/            # Controllers, middleware, Program.cs
  Codebymister.sln
```

## Arquitetura CQRS

O projeto segue o padrão **CQRS (Command Query Responsibility Segregation)**:

### Commands (Escrita)
- Localizados em `Application/UseCases/{Contexto}/Commands/`
- Usam **repositories** para operações de escrita (Create, Update, Delete)
- Trabalham com entidades de domínio completas
- Chamam `UnitOfWork.SaveChangesAsync()` para persistir mudanças

### Queries (Leitura)
- Localizados em `Application/UseCases/{Contexto}/Queries/`
- Usam **query services** (`ILeadQueries`, `IMaintenanceQueries`, etc.)
- Implementações em `Infrastructure/Persistence/Queries/`
- Fazem projeção **direta para DTOs** usando `AsNoTracking()`
- Otimizadas para leitura sem carregar entidades completas

### Benefícios
- Separação clara entre operações de leitura e escrita
- Queries otimizadas sem overhead do tracking do EF Core
- Projeções diretas para DTOs evitam mapeamentos desnecessários
- Facilita evolução independente de leitura e escrita

## Padrões atuais

- API versionada com base em URL (`/api/v1/...`)
- Injeção de dependências por extensions (`AddServices`, `AddUseCases`, `AddRepositories`, `AddQueries`)
- `UnitOfWork` no padrão enxuto: apenas `SaveChangesAsync`
- Commands usam repositórios + UnitOfWork para escrita
- Queries usam query services para leitura otimizada
- Soft-delete + auditoria no `ApplicationDbContext`

## Autenticação

Fluxo atual:
1. Front faz login no Firebase.
2. Backend valida `firebaseIdToken` em `POST /api/v1/auth/exchange-token`.
3. Backend retorna JWT próprio da aplicação (`AuthSession`).
4. Front usa JWT nos endpoints protegidos.
5. Refresh via `POST /api/v1/auth/refresh`.

## Configuração

Arquivo principal: `Codebymister.API/appsettings.json`.

Chaves principais:

- `ConnectionStrings:DefaultConnection`
- `Firebase:ProjectId`
- `FIREBASE_SERVICE_ACCOUNT_BASE64` (quando usado)
- `AppJwt:Issuer`
- `AppJwt:Audience`
- `AppJwt:Key`
- `AppJwt:AccessTokenMinutes`

## Executar localmente

```bash
cd backend
dotnet restore
dotnet build Codebymister.sln
dotnet run --project Codebymister.API
```

- API: `http://localhost:5000`
- Swagger (Development): `http://localhost:5000/swagger`

## Endpoints principais

- `POST /api/v1/auth/exchange-token`
- `POST /api/v1/auth/refresh`
- `GET|POST|PUT|DELETE /api/v1/leads`
- `GET|POST|PUT|DELETE /api/v1/outreaches`
- `GET|POST|PUT|DELETE /api/v1/conversations`
- `GET|POST|PUT|DELETE /api/v1/proposals`
- `GET|POST|PUT|DELETE /api/v1/projects`
- `GET|POST|PUT|DELETE /api/v1/maintenances`
- `GET /api/v1/dashboard`
