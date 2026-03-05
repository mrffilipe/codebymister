# CodeByMister

Sistema interno de **prospecção, vendas e gestão de clientes** (leads → outreach → conversa → proposta → projeto → manutenção).

## Stack Tecnológica

- **Backend**: .NET 8 com Clean Architecture + CQRS
- **Frontend**: React 19 + TypeScript + Vite + Material UI
- **Banco de Dados**: MySQL
- **Autenticação**: Firebase Auth + JWT próprio da aplicação

## Estrutura do Repositório

```text
codebymister/
  backend/              # API REST em .NET 8
    Codebymister.Domain/
    Codebymister.Application/
    Codebymister.Infrastructure/
      └── Persistence/
          ├── Repositories/  # Escrita
          └── Queries/       # Leitura otimizada
    Codebymister.API/
  frontend/             # SPA em React + TypeScript + Vite + MUI
  code-by-mister/       # Projeto institucional (legado)
```

## Documentação Detalhada

- **Backend**: `backend/README.md` - Arquitetura CQRS, autenticação, configuração
- **Frontend**: `frontend/README.md` - Variáveis de ambiente, fluxo de autenticação, componentes

---

## Arquitetura Backend (CQRS)

O backend segue o padrão **CQRS** (Command Query Responsibility Segregation):

- **Commands** (Escrita): Use cases em `Application/UseCases/{Contexto}/Commands/`
  - Usam repositories para operações de escrita
  - Trabalham com entidades de domínio completas
  - Persistem via `UnitOfWork.SaveChangesAsync()`

- **Queries** (Leitura): Use cases em `Application/UseCases/{Contexto}/Queries/`
  - Usam query services (`ILeadQueries`, `IMaintenanceQueries`, etc.)
  - Implementações em `Infrastructure/Persistence/Queries/`
  - Projeção direta para DTOs com `AsNoTracking()`

## Arquitetura Frontend

- **Autenticação**: Login via Firebase → troca por JWT próprio via `/auth/exchange-token`
- **Interceptor Axios**: Adiciona JWT em todas as requisições, refresh automático em 401
- **Estrutura**: Components, Contexts, Services, Pages, Types, Utils
- **Variáveis de Ambiente**: Todas as variáveis devem ter prefixo `VITE_` (obrigatório)

---

## Execução rápida

### Backend

```bash
cd backend
dotnet restore
dotnet run --project Codebymister.API
```

- API: `http://localhost:5000`
- Swagger: `http://localhost:5000/swagger`

### Frontend

```bash
cd frontend
npm install
npm run dev
```

- App: `http://localhost:3002` (ou 3001 se disponível)
- Landing Page: `http://localhost:3002/`
- Dashboard: `http://localhost:3002/dashboard`

---

## Configuração

### Backend (`appsettings.json`)

- `ConnectionStrings:DefaultConnection` - String de conexão MySQL
- `Firebase:ProjectId` - ID do projeto Firebase
- `FIREBASE_SERVICE_ACCOUNT_BASE64` - Credenciais Firebase (base64)
- `AppJwt:Issuer`, `AppJwt:Audience`, `AppJwt:Key` - Configuração JWT
- `AppJwt:AccessTokenMinutes` - Tempo de expiração do token

### Frontend (`.env`)

Todas as variáveis devem ter prefixo `VITE_`:

- `VITE_API_URL` - URL da API backend (ex: `http://localhost:5000/api/v1`)
- `VITE_FIREBASE_API_KEY` - Chave da API Firebase
- `VITE_FIREBASE_AUTH_DOMAIN` - Domínio de autenticação
- `VITE_FIREBASE_PROJECT_ID` - ID do projeto
- `VITE_FIREBASE_STORAGE_BUCKET` - Bucket de storage
- `VITE_FIREBASE_MESSAGING_SENDER_ID` - ID do sender
- `VITE_FIREBASE_APP_ID` - ID da aplicação

---

## Fluxo Funcional

1. **Lead** - Cadastro de potenciais clientes
2. **Outreach** - Registro de tentativas de contato
3. **Conversation** - Registro de conversas e interesse
4. **Proposal** - Criação e envio de propostas
5. **Project** - Gestão de projetos fechados
6. **Maintenance** - Contratos de manutenção recorrente
7. **Dashboard** - Visão geral de métricas e KPIs

---

## Endpoints Principais

- `POST /api/v1/auth/exchange-token` - Troca token Firebase por JWT
- `POST /api/v1/auth/refresh` - Refresh do JWT
- `GET|POST|PUT|DELETE /api/v1/leads` - Gestão de leads
- `GET|POST|PUT|DELETE /api/v1/outreaches` - Gestão de outreach
- `GET|POST|PUT|DELETE /api/v1/conversations` - Gestão de conversas
- `GET|POST|PUT|DELETE /api/v1/proposals` - Gestão de propostas
- `GET|POST|PUT|DELETE /api/v1/projects` - Gestão de projetos
- `GET|POST|PUT|DELETE /api/v1/maintenances` - Gestão de manutenções
- `GET /api/v1/dashboard` - Métricas e KPIs
