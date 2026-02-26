using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class DocumentChunkConfiguration : IEntityTypeConfiguration<DocumentChunk>
    {
        public void Configure(EntityTypeBuilder<DocumentChunk> builder)
        {
            builder.ToTable("DocumentChunks");

            builder.HasKey(dc => dc.Id);
            builder.Property(dc => dc.Id).ValueGeneratedOnAdd();

            builder.Property(dc => dc.Content)
                .IsRequired()
                .HasMaxLength(4000);

            builder.Property(dc => dc.ChunkIndex)
                .IsRequired();

            builder.Property(dc => dc.TokenCount)
                .IsRequired();

            // IMPORTANT : EmbeddingVector stocké comme string JSON
            builder.Property(dc => dc.EmbeddingVector)
                .HasColumnType("nvarchar(max)");

            builder.Property(dc => dc.PageNumber);

            builder.Property(dc => dc.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // Index
            builder.HasIndex(dc => dc.DocumentId);
            builder.HasIndex(dc => dc.ChunkIndex);

            // Relations
            builder.HasOne(dc => dc.Document)
                .WithMany(d => d.DocumentChunks)
                .HasForeignKey(dc => dc.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(dc => dc.MessageReferences)
                .WithOne(mr => mr.DocumentChunk)
                .HasForeignKey(mr => mr.DocumentChunkId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}