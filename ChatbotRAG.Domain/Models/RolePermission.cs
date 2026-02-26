using System;

namespace Domain.Models
{
    /// <summary>
    /// Table de liaison Many-to-Many entre Role et Permission
    /// </summary>
    public class RolePermission
    {
        public int RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;

        public int PermissionId { get; set; }
        public virtual Permission Permission { get; set; } = null!;

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}
