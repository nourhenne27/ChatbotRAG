using System;
using System.Collections.Generic;

namespace Domain.Models
{
    /// <summary>
    /// Représente un échange (question/réponse) dans une conversation
    /// Entité métier fondamentale pour l'historique du chat
    /// </summary>
    public class Message
    {
        public int Id { get; set; }

        public int ConversationId { get; set; }
        public virtual Conversation Conversation { get; set; } = null!;

        public string Role { get; set; } = string.Empty; // "user" ou "assistant"

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Pour les réponses de l'assistant
        public string? Model { get; set; } // Nom du modèle LLM utilisé

        public int? TokensUsed { get; set; }

        public double? ResponseTime { get; set; } // En secondes

        // Métadonnées RAG
        public string? RetrievedChunks { get; set; } // JSON array des chunks utilisés

        public double? ConfidenceScore { get; set; }

        // Navigation properties
        public virtual ICollection<MessageReferences> MessageReferences { get; set; } = new List<MessageReferences>();
    }
}
