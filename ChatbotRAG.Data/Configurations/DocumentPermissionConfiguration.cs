using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class DocumentPermissionConfiguration : IEntityTypeConfiguration<DocumentPermission>
    {
        public void Configure(EntityTypeBuilder<DocumentPermission> builder)
        {
            builder.ToTable("DocumentPermissions");

            // Clé composite
            builder.HasKey(dp => new { dp.DocumentId, dp.RoleId });

            builder.Property(dp => dp.AssignedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // Relations
            builder.HasOne(dp => dp.Document)
                .WithMany(d => d.DocumentPermissions)
                .HasForeignKey(dp => dp.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dp => dp.Role)
                .WithMany()
                .HasForeignKey(dp => dp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}