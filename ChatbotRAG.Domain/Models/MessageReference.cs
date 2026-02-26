using System;

namespace Domain.Models
{
    /// <summary>
    /// Représente une référence entre un message et un chunk de document utilisé
    /// Permet de tracer quels documents ont été utilisés pour générer une réponse
    /// </summary>
    public class MessageReference
    {
        public int Id { get; set; }

        public int MessageId { get; set; }
        public virtual Message Message { get; set; } = null!;

        public int DocumentChunkId { get; set; }
        public virtual DocumentChunk DocumentChunk { get; set; } = null!;

        public double RelevanceScore { get; set; } // Score de similarité/pertinence

        public int RankPosition { get; set; } // Position dans les résultats de recherche

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
