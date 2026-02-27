# 🤖 Chatbot RAG – Intelligent Internal Assistant

![.NET Core](https://img.shields.io/badge/.NET%20Core-3.1-blue)
![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-green)
![Pattern](https://img.shields.io/badge/Pattern-CQRS-orange)
![Database](https://img.shields.io/badge/Database-SQL%20Server-red)
![AI](https://img.shields.io/badge/AI-RAG%20%2B%20OpenAI-purple)

> 🚀 Intelligent internal chatbot based on **RAG (Retrieval-Augmented Generation)**  
> 🏗 Built with **ASP.NET Core 3.1 + Clean Architecture + CQRS**  
> 🧠 Powered by **OpenAI Embeddings + GPT-4**

---

# 📌 Table of Contents

- [Project Overview](#-project-overview)
- [Features](#-features)
- [Architecture](#-architecture)
- [RAG Pipeline](#-rag-pipeline)
- [Technologies](#-technologies)
- [Project Structure](#-project-structure)
- [Installation Guide](#️-installation-guide)
- [Database Setup](#-database-setup)
- [Configuration](#-configuration)
- [Running the Project](#-running-the-project)
- [API Endpoints](#-api-endpoints)
- [Future Improvements](#-future-improvements)
- [Author](#-author)

---

# 📖 Project Overview

This project is an **Intelligent Internal Chatbot System** designed to help employees retrieve information from internal company documents such as:

- PDF files  
- Word documents  
- Text files  
- Reports & Procedures  

The system uses the **RAG (Retrieval-Augmented Generation)** approach:

1. Retrieve relevant document chunks using vector similarity  
2. Generate contextual responses using GPT  
3. Provide traceable references to document sources  

---

# 🎯 Features

- Document upload (PDF, DOCX, TXT)  
- Automatic text extraction  
- Smart text chunking  
- Embedding generation (OpenAI)  
- Vector similarity search  
- GPT-4 contextual response generation  
- Conversation history management  
- Role-based access control (RBAC)  
- JWT Authentication  
- Clean Architecture + CQRS  
            ┌──────────────────────┐
            │        API Layer     │
            │  Controllers + JWT   │
            └───────────┬──────────┘
                        │
            ┌───────────▼──────────┐
            │      Application     │
            │ Commands / Queries   │
            │ Handlers (MediatR)   │
            └───────────┬──────────┘
                        │
            ┌───────────▼──────────┐
            │     Infrastructure   │
            │ EF Core + Services   │
            └───────────┬──────────┘
                        │
                ┌───────▼───────┐
                │  SQL Server   │
                └───────────────┘
---

# 🏗 Architecture

---

# 🧠 RAG Pipeline

## Phase 1 – Indexing

1. Upload document  
2. Extract text  
3. Split into chunks (~500 tokens)  
4. Generate embeddings  
5. Store in database  

## Phase 2 – Retrieval

1. Generate embedding of user question  
2. Compute cosine similarity  
3. Retrieve Top-K relevant chunks  

## Phase 3 – Generation

1. Build prompt with context + history  
2. Send to GPT-4  
3. Save response  

---

# 🛠 Technologies

### Backend
- ASP.NET Core 3.1
- Entity Framework Core (Code First)
- MediatR (CQRS)
- AutoMapper
- FluentValidation
- Serilog
- JWT Authentication

### AI
- OpenAI API (Embeddings + GPT-4)
- Cosine Similarity

### Database
- SQL Server 2019

---
# 📂 Project Structure
```text
ChatbotRAG/
│
├── Api/
│   ├── Controllers/
│   ├── Middleware/
│   ├── Filters/
│   └── Startup.cs
│
├── Domain/
│   ├── Models/
│   ├── Commands/
│   ├── Queries/
│   ├── Handlers/
│   └── Interfaces/
│
├── Data/
│   ├── Context/
│   ├── Configurations/
│   ├── Repositories/
│   └── Migrations/
│
└── Infra/
    ├── AI Services/
    ├── Document Processing/
    └── Storage/
```
---

# ⚙️ Installation Guide

## 1️⃣ Prerequisites

- Visual Studio 2019
- .NET Core 3.1 SDK
- SQL Server 2019
- Git

---

# 📂 Project Structure
