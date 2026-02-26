using System;

namespace Domain.Models
{
    /// <summary>
    /// Table de liaison Many-to-Many entre User et Role
    /// </summary>
    public class UserRole
    {
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public int RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}
