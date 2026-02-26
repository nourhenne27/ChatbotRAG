using System;

namespace Domain.Models
{
    /// <summary>
    /// Représente un fragment de document pour l'embedding et la recherche vectorielle
    /// Essentiel pour le système RAG
    /// </summary>
    public class DocumentChunk
    {
        public int Id { get; set; }

        public int DocumentId { get; set; }
        public virtual Document Document { get; set; } = null!;

        public string Content { get; set; } = string.Empty;

        public int ChunkIndex { get; set; } // Position du chunk dans le document

        public int StartPosition { get; set; } // Position de début dans le document original

        public int EndPosition { get; set; } // Position de fin dans le document original

        public int TokenCount { get; set; } // Nombre de tokens dans le chunk

        // Embedding vectoriel (stocké comme JSON ou binary)
        public string? EmbeddingVector { get; set; } // JSON array de float

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Métadonnées contextuelles
        public int? PageNumber { get; set; }
        public string? SectionTitle { get; set; }
    }
}