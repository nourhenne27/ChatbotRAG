using System;
using System.Collections.Generic;

namespace Domain.Models
{
    /// <summary>
    /// Représente une session de chat avec l'utilisateur
    /// Entité racine pour gérer l'historique des conversations
    /// </summary>
    public class Conversation
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public string Title { get; set; } = string.Empty; // Titre généré ou défini par l'utilisateur

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastMessageAt { get; set; }

        public bool IsActive { get; set; } = true;

        public int MessageCount { get; set; } = 0;

        // Métadonnées de la conversation
        public string? SessionContext { get; set; } // JSON pour stocker le contexte de la session

        // Navigation properties
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}