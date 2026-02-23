# 🤖 ChatbotRAG — Intelligent Internal Assistant

> AI-powered internal chatbot based on **RAG (Retrieval Augmented Generation)** architecture,  
> built with **.NET 5 Web API** backend and **Angular** frontend.

---

## 📋 Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Database Schema](#database-schema)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [API Endpoints](#api-endpoints)
- [How RAG Works](#how-rag-works)
- [Authentication & Roles](#authentication--roles)

---

## 📌 Overview

**ChatbotRAG** is an internal enterprise assistant that allows employees to ask questions  
about company documents (PDF, DOCX) and get accurate, context-based answers powered by a Large Language Model.

### Key Features

- 📄 **Document Indexing** — Upload and index PDF/DOCX files automatically
- 🔍 **Semantic Search** — Vector similarity search using embeddings
- 🧠 **RAG Pipeline** — Retrieval-Augmented Generation for accurate answers
- 💬 **Chat with History** — Conversation sessions with full message history
- 🔐 **Role-Based Access** — Employé / Administrateur / SuperAdmin
- 📊 **Feedback System** — Users can rate each response

---

## 🏗️ Architecture

```
┌─────────────────────────────────────────────────────┐
│                   Angular Frontend                   │
│              (Chat UI + Document Upload)             │
└─────────────────────┬───────────────────────────────┘
                      │ HTTP / REST API
┌─────────────────────▼───────────────────────────────┐
│              .NET 5 Web API (Backend)                │
│                                                      │
│  ┌──────────┐  ┌──────────┐  ┌────────────────────┐ │
│  │   Auth   │  │   Chat   │  │ Document Controller │ │
│  │Controller│  │Controller│  │   (Upload/Index)    │ │
│  └────┬─────┘  └────┬─────┘  └─────────┬──────────┘ │
│       │             │                   │            │
│  ┌────▼─────────────▼───────────────────▼──────────┐ │
│  │              CQRS — MediatR                      │ │
│  │     Commands │ Queries │ Handlers                │ │
│  └────┬─────────────────────────────────┬──────────┘ │
│       │                                 │            │
│  ┌────▼──────────┐          ┌───────────▼──────────┐ │
│  │  RAG Pipeline │          │   SQL Server (EF Core)│ │
│  │  ┌──────────┐ │          │  Users, Sessions,    │ │
│  │  │Extraction│ │          │  Messages, Documents, │ │
│  │  │(PDF/DOCX)│ │          │  Chunks, Vectors     │ │
│  │  └────┬─────┘ │          └──────────────────────┘ │
│  │  ┌────▼─────┐ │                                   │
│  │  │Embedding │ │◄── OpenAI text-embedding-3-small   │
│  │  └────┬─────┘ │                                   │
│  │  ┌────▼─────┐ │                                   │
│  │  │ Retrieval│ │  Cosine Similarity (C#)            │
│  │  └────┬─────┘ │                                   │
│  │  ┌────▼─────┐ │                                   │
│  │  │   LLM    │ │◄── OpenAI GPT-4o-mini              │
│  │  └──────────┘ │                                   │
│  └───────────────┘                                   │
└─────────────────────────────────────────────────────┘
```

---

## 🛠️ Tech Stack

### Backend
| Technology | Version | Usage |
|-----------|---------|-------|
| .NET | 5.0 | Web API Framework |
| ASP.NET Core Identity | 5.0 | Authentication & User Management |
| Entity Framework Core | 5.0 | ORM — Code First |
| SQL Server | 2019 | Main Database |
| MediatR | 9.0 | CQRS Pattern |
| AutoMapper | 8.0 | DTO Mapping |
| Swashbuckle | 5.6 | Swagger API Docs |
| PdfPig | 0.1.7 | PDF Text Extraction |
| OpenXml SDK | 2.18 | DOCX Text Extraction |
| Azure.AI.OpenAI | latest | LLM + Embeddings |
| JWT Bearer | 5.0 | Token Authentication |

### Frontend
| Technology | Usage |
|-----------|-------|
| Angular | Chat UI |
| TypeScript | Language |
| TailwindCSS | Styling |

---

## 📁 Project Structure

```
ChatbotRAG/
│
├── ChatbotRAG.Api/                  # ASP.NET Core Web API
│   ├── Controllers/
│   │   ├── AuthController.cs        # Login, Register, JWT
│   │   ├── ChatController.cs        # Send message, Get history
│   │   └── DocumentController.cs   # Upload, Index documents
│   ├── appsettings.json
│   ├── Program.cs
│   └── Startup.cs
│
├── ChatbotRAG.Domain/               # Business Logic (CQRS)
│   ├── Models/
│   │   ├── AppUser.cs               # Utilisateur (TPH: Employé/Admin/SuperAdmin)
│   │   ├── Session.cs               # Chat session
│   │   ├── Message.cs               # Chat message
│   │   ├── Document.cs              # Uploaded document
│   │   ├── Chunk.cs                 # Text chunk + embedding
│   │   ├── LogSysteme.cs            # System logs
│   │   └── ...
│   ├── Commands/
│   │   ├── UploadDocumentCommand.cs
│   │   ├── SendMessageCommand.cs
│   │   └── CreateSessionCommand.cs
│   ├── Queries/
│   │   ├── GetSessionQuery.cs
│   │   └── GetAllSessionsQuery.cs
│   ├── Handlers/
│   │   ├── UploadDocumentHandler.cs
│   │   ├── SendMessageHandler.cs
│   │   └── ...
│   └── Interface/
│       ├── IDocumentRepository.cs
│       ├── ISessionRepository.cs
│       ├── IEmbeddingService.cs
│       └── ILLMService.cs
│
├── ChatbotRAG.Data/                 # Data Access Layer
│   ├── Context/
│   │   └── AppDbContext.cs          # EF Core DbContext
│   └── Repositories/
│       ├── DocumentRepository.cs
│       └── SessionRepository.cs
│
└── ChatbotRAG.Infra/                # External Services
    └── Services/
        ├── OpenAIEmbeddingService.cs
        ├── OpenAILLMService.cs
        └── DocumentExtractorService.cs
```

---

## 🗄️ Database Schema

```
AspNetUsers          → AppUser (Identity + TPH: Employé/Admin/SuperAdmin)
Sessions             → Chat sessions per user
Messages             → Messages inside a session
Documents            → Uploaded documents
Chunks               → Text chunks from documents (with EmbeddingJson)
LogSystemes          → System action logs
```

> **Note:** Embeddings are stored as JSON strings (`nvarchar(max)`) in SQL Server.  
> Cosine similarity is computed in C# at retrieval time.

---

## 🚀 Getting Started

### Prerequisites

- [Visual Studio 2019+](https://visualstudio.microsoft.com/)
- [SQL Server 2019](https://www.microsoft.com/sql-server)
- [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Node.js](https://nodejs.org/) (for Angular frontend)
- OpenAI API Key

### Installation

**1. Clone the repository**
```bash
git clone https://github.com/your-username/ChatbotRAG.git
cd ChatbotRAG
```

**2. Configure appsettings.json**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=ChatbotRAG;Trusted_Connection=True;"
  },
  "OpenAI": {
    "ApiKey": "sk-your-api-key-here"
  },
  "Jwt": {
    "Key": "YourSuperSecretKey32CharactersMin!!",
    "Issuer": "ChatbotRAG",
    "Audience": "ChatbotRAGUsers"
  }
}
```

**3. Run database migrations**
```bash
# In Visual Studio — NuGet Package Manager Console
PM> add-migration InitialCreate
PM> update-database
```

**4. Run the API**
```bash
dotnet run --project ChatbotRAG.Api
# API available at: https://localhost:5001
# Swagger UI at:    https://localhost:5001/swagger
```

---

## ⚙️ Configuration

| Key | Description |
|-----|-------------|
| `ConnectionStrings:DefaultConnection` | SQL Server connection string |
| `OpenAI:ApiKey` | Your OpenAI API key |
| `Jwt:Key` | Secret key for JWT token signing (min 32 chars) |
| `Jwt:Issuer` | JWT issuer name |
| `Jwt:Audience` | JWT audience name |

---

## 📡 API Endpoints

### Auth
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/auth/register` | Register new user |
| POST | `/api/auth/login` | Login and get JWT token |

### Chat
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/chat/session` | Create new session |
| POST | `/api/chat/message` | Send message (triggers RAG) |
| GET | `/api/chat/session/{id}` | Get session with messages |
| GET | `/api/chat/sessions` | Get all sessions for current user |

### Documents
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/document/upload` | Upload and index a document |
| GET | `/api/document` | List all documents |
| DELETE | `/api/document/{id}` | Delete a document |

---

## 🧠 How RAG Works

```
1. INDEXING (on document upload)
   ┌──────────┐    ┌──────────┐    ┌──────────┐    ┌──────────┐
   │ PDF/DOCX │───►│ Extract  │───►│  Split   │───►│ Embed &  │
   │  Upload  │    │  Text    │    │  Chunks  │    │  Store   │
   └──────────┘    └──────────┘    └──────────┘    └──────────┘

2. QUERYING (on user message)
   ┌──────────┐    ┌──────────┐    ┌──────────┐    ┌──────────┐
   │ User     │───►│ Embed    │───►│ Find Top │───►│ Build    │
   │ Question │    │ Question │    │ 5 Chunks │    │ Prompt   │
   └──────────┘    └──────────┘    └──────────┘    └────┬─────┘
                                                        │
                                                   ┌────▼─────┐
                                                   │   LLM    │
                                                   │ GPT-4o   │
                                                   └────┬─────┘
                                                        │
                                                   ┌────▼─────┐
                                                   │ Response │
                                                   │ + Save   │
                                                   └──────────┘
```

---

## 🔐 Authentication & Roles

The system uses **ASP.NET Core Identity** with **JWT Bearer tokens** and **TPH (Table Per Hierarchy)** pattern.

| Role | Permissions |
|------|------------|
| **Employé** | Chat, view own sessions, give feedback |
| **Administrateur** | + Manage users, view logs, manage documents |
| **SuperAdmin** | + Configure LLM API keys, manage all permissions |

> **Note:** One user is exclusively ONE role — never multiple (as defined in the class diagram).

---

## 👩‍💻 Author

**Developed by:** Mme Souha Ben JEDDI  
**Organization:** PGH — Unité Développement  
**Year:** 2022–2024

---

## 📄 License

This project is proprietary and for internal use only.  
© PGH — Poulina Group Holding. All rights reserved.
