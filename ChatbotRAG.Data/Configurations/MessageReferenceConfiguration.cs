using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class MessageReferenceConfiguration : IEntityTypeConfiguration<MessageReference>
    {
        public void Configure(EntityTypeBuilder<MessageReference> builder)
        {
            builder.ToTable("MessageReferences");

            builder.HasKey(mr => mr.Id);
            builder.Property(mr => mr.Id).ValueGeneratedOnAdd();

            builder.Property(mr => mr.RelevanceScore)
                .IsRequired()
                .HasColumnType("decimal(5,4)");

            builder.Property(mr => mr.RankPosition)
                .IsRequired();

            builder.Property(mr => mr.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // Index
            builder.HasIndex(mr => mr.MessageId);
            builder.HasIndex(mr => mr.DocumentChunkId);

            // Relations
            builder.HasOne(mr => mr.Message)
                .WithMany(m => m.MessageReferences)
                .HasForeignKey(mr => mr.MessageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(mr => mr.DocumentChunk)
                .WithMany(dc => dc.MessageReferences)
                .HasForeignKey(mr => mr.DocumentChunkId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
