using System;

namespace Domain.Models
{
    /// <summary>
    /// Gère les permissions d'accès aux documents par rôle
    /// </summary>
    public class DocumentPermission
    {
        public int Id { get; set; }

        public int DocumentId { get; set; }
        public virtual Document Document { get; set; } = null!;

        public int RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;

        public bool CanRead { get; set; } = true;
        public bool CanUpdate { get; set; } = false;
        public bool CanDelete { get; set; } = false;

        public DateTime GrantedAt { get; set; } = DateTime.UtcNow;
    }
}