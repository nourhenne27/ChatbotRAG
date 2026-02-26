using System;
using System.Collections.Generic;

namespace Domain.Models
{
    /// <summary>
    /// Représente un document source indexé (PDF, Word, etc.)
    /// Entité racine pour le système RAG
    /// </summary>
    public class Document
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public string FileType { get; set; } = string.Empty; // PDF, DOCX, TXT, etc.

        public long FileSize { get; set; } // En octets

        public string ContentHash { get; set; } = string.Empty; // Hash du contenu pour détecter les doublons

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public DateTime? IndexedAt { get; set; }

        public bool IsIndexed { get; set; } = false;

        public string Status { get; set; } = "Pending"; // Pending, Processing, Indexed, Failed

        public string? ErrorMessage { get; set; }

        public int? UploadedByUserId { get; set; }
        public virtual User? UploadedBy { get; set; }

        // Métadonnées
        public string? Category { get; set; }
        public string? Department { get; set; }
        public string? Tags { get; set; } // JSON array ou comma-separated

        // Navigation properties
        public virtual ICollection<DocumentChunk> Chunks { get; set; } = new List<DocumentChunk>();

        public virtual ICollection<DocumentPermission> DocumentPermissions { get; set; } = new List<DocumentPermission>();
    }
}
