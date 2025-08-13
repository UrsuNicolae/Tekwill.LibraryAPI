using FluentValidation;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Data.Configurations
{
    public class ChatConfigurations : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable("chats");
            builder.HasKey(b => b.Id);

            builder.Property(p => p.FirtName)
                .HasMaxLength(256);

            builder.Property(p => p.LastName)
                .HasMaxLength(256);

            builder.Property(p => p.UserName)
                .HasMaxLength(256)
                .HasDefaultValue(null);

            builder.Property(b => b.IsForm)
                .HasDefaultValue(false);
            builder.Property(b => b.Type)
                .HasMaxLength(100);

            builder.HasMany(p => p.ChatNotifications)
                .WithOne(b => b.Chat)
                .HasForeignKey(b => b.ChatId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
