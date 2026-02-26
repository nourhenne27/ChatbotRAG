using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.Description)
                .HasMaxLength(255);

            builder.Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // Index
            builder.HasIndex(r => r.Name).IsUnique();

            // Relations
            builder.HasMany(r => r.RolePermissions)
                .WithOne(rp => rp.Role)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Data - Rôles par défaut
            builder.HasData(
                new Role { Id = 1, Name = "SuperAdmin", Description = "Administrateur système", CreatedAt = DateTime.UtcNow },
                new Role { Id = 2, Name = "Admin", Description = "Administrateur", CreatedAt = DateTime.UtcNow },
                new Role { Id = 3, Name = "Employee", Description = "Employé", CreatedAt = DateTime.UtcNow }
            );
        }
    }
}