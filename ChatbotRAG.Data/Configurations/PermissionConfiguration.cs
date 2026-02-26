using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .HasMaxLength(255);

            builder.Property(p => p.Category)
                .HasMaxLength(50);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // Index
            builder.HasIndex(p => p.Code).IsUnique();

            // Relations
            builder.HasMany(p => p.RolePermissions)
                .WithOne(rp => rp.Permission)
                .HasForeignKey(rp => rp.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Data - Permissions par défaut
            builder.HasData(
                // Documents
                new Permission { Id = 1, Code = "DOCUMENT_VIEW", Name = "Voir documents", Category = "Documents", CreatedAt = DateTime.UtcNow },
                new Permission { Id = 2, Code = "DOCUMENT_UPLOAD", Name = "Upload documents", Category = "Documents", CreatedAt = DateTime.UtcNow },
                new Permission { Id = 3, Code = "DOCUMENT_DELETE", Name = "Supprimer documents", Category = "Documents", CreatedAt = DateTime.UtcNow },

                // Chat
                new Permission { Id = 4, Code = "CHAT_USE", Name = "Utiliser le chat", Category = "Chat", CreatedAt = DateTime.UtcNow },
                new Permission { Id = 5, Code = "CHAT_HISTORY", Name = "Voir historique chat", Category = "Chat", CreatedAt = DateTime.UtcNow },

                // Users
                new Permission { Id = 6, Code = "USER_MANAGE", Name = "Gérer utilisateurs", Category = "Users", CreatedAt = DateTime.UtcNow },
                new Permission { Id = 7, Code = "ROLE_MANAGE", Name = "Gérer rôles", Category = "Users", CreatedAt = DateTime.UtcNow }
            );
        }
    }
}
