using System.Collections.Generic;

namespace Domain.Models
{
    /// <summary>
    /// Représente un rôle utilisateur pour le contrôle d'accès
    /// </summary>
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsSystemRole { get; set; } = false;

        // Navigation properties
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}