# 🤖 Chatbot RAG — Assistance Interne Intelligente

> Un chatbot d'assistance interne basé sur l'approche **Retrieval Augmented Generation (RAG)**, construit avec **ASP.NET Core 3.1**, **Clean Architecture**, **CQRS**, et **OpenAI GPT-4**.

![.NET](https://img.shields.io/badge/.NET_Core-3.1-512BD4?style=flat-square&logo=dotnet)
![SQL Server](https://img.shields.io/badge/SQL_Server-2019-CC2927?style=flat-square&logo=microsoftsqlserver)


---

## 📋 Table des matières

- [Présentation](#-présentation)
- [Architecture](#-architecture)
- [Stack technique](#-stack-technique)
- [Structure du projet](#-structure-du-projet)
- [Prérequis](#-prérequis)
- [Installation](#-installation)
- [Configuration](#-configuration)
- [Démarrage](#-démarrage)
- [Endpoints & Interfaces](#-endpoints--interfaces)
- [Diagramme de classes](#-diagramme-de-classes)
- [Commandes utiles](#-commandes-utiles)
- [Contribuer](#-contribuer)

---

## 🎯 Présentation

Ce projet implémente un **chatbot d'assistance interne** permettant aux employés d'interroger les documents de l'entreprise (PDF, Word, TXT) en langage naturel.

Le système repose sur le pipeline **RAG** :

1. 📄 **Ingestion** — Les documents internes sont découpés en chunks et encodés en vecteurs 
2. 🔍 **Retrieval** — À chaque question, les chunks les plus pertinents sont retrouvés 
3. 💬 **Generation** —.... génère une réponse contextualisée en s'appuyant sur ces chunks

### Fonctionnalités principales

- Indexation automatique de documents (PDF, DOCX, TXT)
- Recherche sémantique vectorielle
- Génération de réponses avec citation des sources
- Gestion des utilisateurs, rôles et permissions
- Traçabilité complète des conversations et messages
- Interface Angular moderne et responsive
- Documentation API interactive 

---

## 🏗 Architecture

Le projet suit la **Clean Architecture** combinée au pattern **CQRS** (Command Query Responsibility Segregation) avec **MediatR**.

```
┌──────────────────────────────────────────────────────────┐
│                      Angular (UI)                        │
└────────────────────────┬─────────────────────────────────┘
                         │ HTTP / SignalR
┌────────────────────────▼─────────────────────────────────┐
│                    Api (ASP.NET Core)                    │
│               Controllers · Middleware · JWT             │
└────────┬─────────────────────────────────┬───────────────┘
         │ Commands                         │ Queries
┌────────▼──────────────┐    ┌─────────────▼───────────────┐
│   Command Handlers    │    │      Query Handlers          │
│  Validation · Write   │    │   Optimized · DTOs · Read    │
└────────┬──────────────┘    └─────────────┬───────────────┘
         └──────────────┬──────────────────┘
                        │
         ┌──────────────▼──────────────┐
         │       Data (EF Core)        │
         │  Repositories · DbContext   │
         └──────────────┬──────────────┘
                        │
         ┌──────────────▼──────────────┐
         │      SQL Server 2019        │
         └─────────────────────────────┘

         ┌─────────────────────────────┐
         │    Infra (Services IA)      │
         │  OpenAI · Azure Search      │
         │  Document Processing        │
         └─────────────────────────────┘
```

**Règles de dépendance entre les couches :**

```
Domain      ← Aucune dépendance (couche pure)
Data        ← Domain
Infra       ← Domain · Data
Api         ← Domain · Data · Infra
```

---

## 🛠 Stack technique

| Catégorie | Technologie | Version | Rôle |
|-----------|-------------|---------|------|
| **Backend** | ASP.NET Core | 3.1 | Framework API REST |
| | Entity Framework Core | 3.1 | ORM Code First |
| | SQL Server | 2019 | Base de données relationnelle |
| | MediatR | 8.0 | Implémentation CQRS |
| | AutoMapper | 10.0 | Mapping entités ↔ DTOs |
| | FluentValidation | 9.0 | Validation des commandes |
| | Serilog | 2.10 | Logging structuré |
| | JWT Bearer | 3.1 | Authentification |
| | Swashbuckle | 5.2 | Documentation Swagger |
| **IA / ML** |
| **Frontend** | Angular | 
---

## 📁 Structure du projet

```
Solution 'ChatbotRAG'
│
├── 📁 Api                          # Couche présentation
│   ├── Controllers/                # Endpoints REST
│   ├── Filters/                    # Filtres globaux
│   ├── Middleware/                 # Pipeline HTTP
│   ├── appsettings.json
│   ├── Program.cs
│   └── Startup.cs
│
├── 📁 Domain                       # Couche métier (aucune dépendance)
│   ├── Models/                     # Entités (User, Document, Message…)
│   ├── Commands/                   # Commandes CQRS (écriture)
│   ├── Queries/                    # Requêtes CQRS (lecture)
│   ├── Handlers/                   # Handlers MediatR
│   └── Interface/                  # IGenericRepository<T>
│
├── 📁 Data                         # Couche persistance
│   ├── Context/
│   │   └── ApplicationDbContext.cs
│   ├── Repositories/               # Implémentations des repositories
│   └── Migrations/
│
├── 📁 Infra                        # Couche infrastructure / services IA
│   └── Services/
│       ├── OpenAIEmbeddingService.cs
│       ├── VectorSearchService.cs
│       └── DocumentProcessingService.cs
│
└── 📁 frontend                     # Application Angular
    ├── src/
    └── package.json
```

---

## ✅ Prérequis

| Outil | Version minimale |
|-------|-----------------|
| Windows | 10 / 11 |
| Visual Studio | 2019 (charge de travail ASP.NET) |
| .NET Core SDK | 3.1 |
| SQL Server | 2019 Developer Edition |
| Node.js |  |
| Angular CLI |  |
| Git |  |

---


**Entités principales :**

| Entité | Description |
|--------|-------------|
| `User` | Utilisateur avec email, rôles et permissions |
| `Role` / `Permission` | Système RBAC granulaire |
| `Document` | Fichier indexé (PDF, DOCX, TXT) |
| `DocumentChunk` | Fragment de document avec vecteur d'embedding |
| `Conversation` | Session de chat d'un utilisateur |
| `Message` | Message individuel (user ou assistant) |
| `MessageReference` | Source documentaire utilisée pour une réponse |

---

## 📦 

**Version** : 1.0 · **Framework** : ASP.NET Core 3.1 · **Auteur** : Nourhenne ben Abdelghaffar · **Année** : 2026

# 📂 Project Structure
