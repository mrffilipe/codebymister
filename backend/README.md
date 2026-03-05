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

---

## Docker & Deploy

### Desenvolvimento - Build e Push para GitHub Container Registry

**1. Login no GitHub Container Registry:**
```bash
docker login ghcr.io -u mrffilipe
```
> Autentica no GitHub Container Registry usando seu usuário. Você será solicitado a fornecer um Personal Access Token (PAT) com permissões `write:packages`.

**2. Build da imagem Docker:**
```bash
docker build -t ghcr.io/mrffilipe/codebymister-api:1.0.0 -f Codebymister.API/Dockerfile .
```
> Constrói a imagem Docker usando o Dockerfile multi-stage localizado em `Codebymister.API/Dockerfile`. A flag `-t` define a tag da imagem.

**3. Tag como latest:**
```bash
docker tag ghcr.io/mrffilipe/codebymister-api:1.0.0 ghcr.io/mrffilipe/codebymister-api:latest
```
> Cria uma tag adicional `latest` apontando para a mesma imagem versionada. Útil para sempre puxar a versão mais recente sem especificar a versão.

**4. Push para o registry:**
```bash
docker push ghcr.io/mrffilipe/codebymister-api:1.0.0
```
> Envia a imagem versionada para o GitHub Container Registry, tornando-a disponível para deploy em servidores.

**5. Build multi-arquitetura (opcional):**
```bash
docker buildx build --platform linux/amd64,linux/arm64 \
  -t ghcr.io/mrffilipe/codebymister-api:1.0.0 \
  -t ghcr.io/mrffilipe/codebymister-api:latest \
  -f Codebymister.API/Dockerfile . --push
```
> Usa `buildx` para criar imagens compatíveis com múltiplas arquiteturas (AMD64 e ARM64) e faz push direto. Útil para deploy em diferentes tipos de servidores (ex: VPS x86 ou ARM).

---

### Produção - Deploy na VPS

**1. Login no registry:**
```bash
docker login ghcr.io
```
> Autentica no GitHub Container Registry na VPS para puxar a imagem privada.

**2. Inicializar Infisical:**
```bash
infisical init
```
> Inicializa o Infisical no diretório do projeto. O Infisical é uma ferramenta de gerenciamento de secrets que sincroniza variáveis de ambiente de forma segura.

**3. Exportar secrets para .env:**
```bash
infisical secrets --output=dotenv > .env
```
> Busca os secrets configurados no Infisical e gera um arquivo `.env` local. Isso evita hardcoding de credenciais e centraliza a gestão de secrets.

**4. Subir containers:**
```bash
docker compose up -d
```
> Sobe os containers em modo detached usando o `docker-compose.prod.yml`. O arquivo `.env` gerado é automaticamente carregado pelo container.

**5. Verificar logs:**
```bash
docker logs --tail 100 codebymister-api
```
> Exibe as últimas 100 linhas de log do container `codebymister-api`. Útil para debug e verificação de inicialização.

---

## Melhorias Implementadas

### Docker Support
- **Dockerfile multi-stage** otimizado para produção
- **docker-compose.yml** para desenvolvimento local
- **docker-compose.prod.yml** para deploy em produção
- Suporte a User Secrets para desenvolvimento local
- Configuração de portas: 8080 (HTTP) e 8081 (HTTPS)

### Firebase Configuration
- Validação robusta de credenciais (private_key, client_email)
- Suporte a scopes específicos do Firebase (messaging, auth, cloud-platform)
- Serialização JSON com atributos `[JsonPropertyName]` para compatibilidade
- Mensagens de erro mais descritivas

### Infisical Integration
- Gerenciamento seguro de secrets em produção
- Sincronização automática de variáveis de ambiente
- Evita exposição de credenciais no código-fonte
