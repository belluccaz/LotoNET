# 🎯 Loto.NET

Sistema gratuito e open-source de análise de resultados das loterias da Caixa Econômica Federal (Mega-Sena, Quina e Lotofácil). Desenvolvido com ASP.NET Core 8, PostgreSQL e React, o projeto visa fornecer dados atualizados e ferramentas visuais para estudo estatístico dos concursos.

![.NET 8](https://img.shields.io/badge/.NET-8.0-blueviolet) ![PostgreSQL](https://img.shields.io/badge/Database-PostgreSQL-blue) ![React](https://img.shields.io/badge/Frontend-React-61DAFB) ![Status](https://img.shields.io/badge/Status-Em%20Desenvolvimento-yellow)

---

## 📌 Funcionalidades

- Armazenamento e atualização automática dos resultados da Mega-Sena, Quina e Lotofácil
- Exposição de dados via API RESTful
- Consulta por loteria, concurso ou últimos resultados
- Estatísticas: dezenas mais sorteadas e frequência por posição
- Backend robusto com arquitetura limpa e boas práticas

---

## 🛠️ Tecnologias Utilizadas

### Backend

- ASP.NET Core 8 (Web API)
- Entity Framework Core
- PostgreSQL (via Supabase ou local)
- Docker (em breve)
- Arquitetura: Clean Architecture + DDD

### Frontend (em breve)

- React + TypeScript
- TailwindCSS ou CSS Modules

---

## ⚙️ Executando localmente

### 1. Clonar o repositório

```bash
git clone https://github.com/seuusuario/LotoNET.git
cd LotoNET
```

### 2. Configurar variáveis de ambiente

Crie um arquivo `.env` na raiz com as seguintes variáveis:

```env
POSTGRES_HOST=localhost
POSTGRES_PORT=5432
POSTGRES_USER=postgres
POSTGRES_PASSWORD=sua_senha_aqui
POSTGRES_DB=lotonet_db

JWT_SECRET=sua_chave_secreta_segura
ASPNETCORE_ENVIRONMENT=Development
```

> ✅ **Dica**: Use o comando `openssl rand -base64 32` para gerar o `JWT_SECRET`.

### 3. Ajustar `appsettings.json`

```jsonc
{
  "ConnectionStrings": {
    "DefaultConnection": ""
  }
}
```

> A string será montada em tempo de execução com base nas variáveis do `.env`.

---

## ▶️ Executando o Backend

```bash
dotnet run --project LotoNET.API
```

Ao iniciar:

- Os resultados mais recentes das três loterias serão importados da API externa.
- O banco será populado automaticamente, sem necessidade de comandos manuais.

---

## 🔗 Endpoints disponíveis

| Método | Rota                                       | Descrição                            |
| ------ | ------------------------------------------ | ------------------------------------ |
| `GET`  | `/api/loterias`                            | Lista todas as loterias cadastradas  |
| `GET`  | `/api/resultados`                          | Lista todos os resultados            |
| `GET`  | `/api/resultados/{loteria}`                | Resultados de uma loteria específica |
| `GET`  | `/api/resultados/ultimos/{loteria}`        | Últimos 10 concursos                 |
| `GET`  | `/api/resultados/mais-sorteados/{loteria}` | Dezenas mais frequentes              |
| `GET`  | `/api/resultados/posicoes/{loteria}`       | Frequência das dezenas por posição   |
| `GET`  | `/api/resultados/{loteria}/{concurso}`     | Detalhes de um concurso específico   |

---

## 📁 Estrutura do Projeto

```
LotoNET
├── LotoNET.API               // Camada de apresentação (controllers, middlewares, startup)
├── LotoNET.Application       // DTOs, interfaces, mapeamentos
├── LotoNET.Domain            // Entidades e regras de negócio
├── LotoNET.Infrastructure    // Persistência (EF Core), serviços e conexões externas
├── data                      // (opcional) Arquivos JSON estáticos para testes locais
└── .env                      // Configurações locais (não subir para o Git)
```

---

## 💡 Objetivo do Projeto

Este projeto nasceu da vontade de facilitar os estudos estatísticos de loteria — como faz a mãe do autor — oferecendo uma ferramenta moderna, acessível e open-source para consulta e visualização dos dados.

---

## 🧠 Autor

**Lucas Bellucci Almendra**  
[![LinkedIn](https://img.shields.io/badge/-LinkedIn-blue?style=flat&logo=linkedin)](https://www.linkedin.com/in/lucas-bellucci-353b10298)  
Desenvolvedor .NET apaixonado por desafios técnicos e soluções que fazem sentido de verdade.

---

## 📝 Licença

Este projeto está sob a licença MIT.
