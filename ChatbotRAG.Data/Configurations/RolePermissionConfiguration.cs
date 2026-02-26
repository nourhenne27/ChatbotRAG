using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermissions");

            // Clé composite
            builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });

            builder.Property(rp => rp.AssignedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // Relations
            builder.HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Data - Permissions par rôle
            builder.HasData(
                // SuperAdmin = Toutes permissions
                new RolePermission { RoleId = 1, PermissionId = 1, AssignedAt = DateTime.UtcNow },
                new RolePermission { RoleId = 1, PermissionId = 2, AssignedAt = DateTime.UtcNow },
                new RolePermission { RoleId = 1, PermissionId = 3, AssignedAt = DateTime.UtcNow },
                new RolePermission { RoleId = 1, PermissionId = 4, AssignedAt = DateTime.UtcNow },
                new RolePermission { RoleId = 1, PermissionId = 5, AssignedAt = DateTime.UtcNow },
                new RolePermission { RoleId = 1, PermissionId = 6, AssignedAt = DateTime.UtcNow },
                new RolePermission { RoleId = 1, PermissionId = 7, AssignedAt = DateTime.UtcNow },

                // Admin = Quelques permissions
                new RolePermission { RoleId = 2, PermissionId = 1, AssignedAt = DateTime.UtcNow },
                new RolePermission { RoleId = 2, PermissionId = 2, AssignedAt = DateTime.UtcNow },
                new RolePermission { RoleId = 2, PermissionId = 4, AssignedAt = DateTime.UtcNow },
                new RolePermission { RoleId = 2, PermissionId = 5, AssignedAt = DateTime.UtcNow },

                // Employee = Permissions de base
                new RolePermission { RoleId = 3, PermissionId = 1, AssignedAt = DateTime.UtcNow },
                new RolePermission { RoleId = 3, PermissionId = 4, AssignedAt = DateTime.UtcNow }
            );
        }
    }
}