# CodeByMister

Frontend do sistema CodeByMister, construído com **React + TypeScript + Vite + MUI**.

## Stack

- React 19
- TypeScript
- Vite
- Material UI
- Axios
- Firebase Auth (login)

## Autenticação (fluxo atual)

O frontend **não usa o token Firebase diretamente nas APIs de negócio**.

Fluxo:
1. Usuário faz login via Firebase.
2. Front chama `POST /auth/exchange-token` com `firebaseIdToken`.
3. Backend retorna `AuthSession` com `accessToken` JWT próprio.
4. Axios envia esse JWT no header `Authorization`.
5. Em `401`, o frontend tenta `POST /auth/refresh` e repete a request.

Arquivos principais:
- `src/contexts/AuthContext.tsx`
- `src/config/axios.ts`
- `src/services/authService.ts`
- `src/utils/authStorage.ts`

## Variáveis de Ambiente

Crie um arquivo `.env` na raiz do projeto com as seguintes variáveis:

```env
# URL base da API backend (incluindo o prefixo de versão)
VITE_API_URL=http://localhost:5000/api/v1

# Configurações do Firebase (obtidas no console do Firebase)
VITE_FIREBASE_API_KEY=
VITE_FIREBASE_AUTH_DOMAIN=
VITE_FIREBASE_PROJECT_ID=
VITE_FIREBASE_STORAGE_BUCKET=
VITE_FIREBASE_MESSAGING_SENDER_ID=
VITE_FIREBASE_APP_ID=
```

> **Atenção:** todas as variáveis devem ter o prefixo `VITE_` para serem expostas pelo Vite via `import.meta.env`.
> Sem esse prefixo, os valores serão `undefined` em tempo de execução.

Se `VITE_API_URL` não for definida, o axios usa o fallback `http://localhost:5000/api/v1`.

## Scripts

```bash
npm install
npm run dev
npm run build
```

## Estrutura (resumo)

```text
src/
  components/
    Layout/              # MainLayout (dashboard)
    StepperDialog.tsx    # Formulários em etapas
  config/
    axios.ts             # Configuração Axios + interceptors
    firebase.ts          # Configuração Firebase
  contexts/
    AuthContext.tsx      # Contexto de autenticação
  pages/
    Landing/             # Página institucional (raiz /)
      components/        # Header, Footer
      sections/          # Hero, About, Projects, etc.
    Dashboard.tsx        # Dashboard com métricas e gráficos
    Leads.tsx            # Gestão de leads
    Outreaches.tsx       # Gestão de abordagens
    Conversations.tsx    # Gestão de conversas
    Proposals.tsx        # Gestão de propostas
    Projects.tsx         # Gestão de projetos
    Maintenances.tsx     # Gestão de manutenções
    Login.tsx            # Página de login
  services/              # Serviços de API
  types/                 # Tipos TypeScript
  utils/
    masks.ts             # Máscaras (telefone, instagram)
    validations.ts       # Schemas Yup
    tableResponsive.tsx  # Helper para tabelas responsivas
```

## Rotas

- `/` - Landing page institucional (pública)
- `/login` - Página de login
- `/dashboard` - Dashboard com métricas (protegida)
- `/dashboard/leads` - Gestão de leads (protegida)
- `/dashboard/outreach` - Gestão de abordagens (protegida)
- `/dashboard/conversations` - Gestão de conversas (protegida)
- `/dashboard/proposals` - Gestão de propostas (protegida)
- `/dashboard/projects` - Gestão de projetos (protegida)
- `/dashboard/maintenance` - Gestão de manutenções (protegida)

## Funcionalidades Implementadas

### Landing Page
- Design moderno com MUI v7
- Seções: Hero, About, Projects, Certificates, Experience, Contact
- Totalmente responsiva
- Animações suaves
- Link discreto para dashboard no footer

### Dashboard
- 9 cards de métricas (3x3)
- 3 gráficos interativos (Funil de Vendas, Receita Mensal, Origem dos Leads)
- Layout responsivo

### Formulários
- Sistema de etapas com `StepperDialog`
- Máscaras automáticas (telefone, instagram)
- Validações com Yup
- Navegação: Voltar, Avançar, Salvar, Cancelar
- Responsivos (fullscreen em mobile)

### Autenticação
- Login via Firebase
- Troca por JWT próprio
- Refresh automático de token
- Proteção de rotas

## Observações

- Os serviços em `src/services/*` estão alinhados aos endpoints versionados do backend (`/api/v1/...`).
- Os tipos em `src/types/*` foram ajustados para refletir nullability e enums do backend.
- Todas as páginas do dashboard são responsivas para mobile.
- Formulários seguem padrão de etapas para melhor UX.
