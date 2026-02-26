using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");

            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).ValueGeneratedOnAdd();

            builder.Property(d => d.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(d => d.FileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(d => d.FilePath)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(d => d.FileType)
                .HasMaxLength(10);

            builder.Property(d => d.FileSize)
                .IsRequired();

            builder.Property(d => d.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("Pending");

            builder.Property(d => d.Category)
                .HasMaxLength(50);

            builder.Property(d => d.Tags)
                .HasMaxLength(500);

            builder.Property(d => d.UploadedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(d => d.ProcessedAt);

            builder.Property(d => d.IsActive)
                .HasDefaultValue(true);

            // Index
            builder.HasIndex(d => d.Status);
            builder.HasIndex(d => d.Category);
            builder.HasIndex(d => d.UploadedAt);

            // Relations
            builder.HasOne(d => d.UploadedBy)
                .WithMany(u => u.Documents)
                .HasForeignKey(d => d.UploadedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.DocumentChunks)
                .WithOne(dc => dc.Document)
                .HasForeignKey(dc => dc.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(d => d.DocumentPermissions)
                .WithOne(dp => dp.Document)
                .HasForeignKey(dp => dp.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
