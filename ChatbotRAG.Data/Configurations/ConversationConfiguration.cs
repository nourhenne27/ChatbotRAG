using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.ToTable("Conversations");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Title)
                .HasMaxLength(255);

            builder.Property(c => c.StartedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(c => c.LastMessageAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(c => c.IsActive)
                .HasDefaultValue(true);

            builder.Property(c => c.MessageCount)
                .HasDefaultValue(0);

            builder.Property(c => c.SessionContext)
                .HasColumnType("nvarchar(max)");

            // Index
            builder.HasIndex(c => c.UserId);
            builder.HasIndex(c => c.StartedAt);
            builder.HasIndex(c => c.IsActive);

            // Relations
            builder.HasOne(c => c.User)
                .WithMany(u => u.Conversations)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Messages)
                .WithOne(m => m.Conversation)
                .HasForeignKey(m => m.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}