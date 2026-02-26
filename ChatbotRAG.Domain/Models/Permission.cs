using System.Collections.Generic;

namespace Domain.Models
{
    /// <summary>
    /// Représente une permission granulaire sur les documents/fonctionnalités
    /// </summary>
    public class Permission
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty; // Ex: "documents.read", "chat.access", "documents.upload"

        public string Description { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty; // Ex: "Documents", "Chat", "Administration"

        // Navigation properties
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    } 
}