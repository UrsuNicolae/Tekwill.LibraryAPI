using Library.Domain.Entities;
using Library.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Library.Infrastructure.Data.Configurations
{
    public class ChatNotificationConfigurations : IEntityTypeConfiguration<ChatNotifications>
    {
        public void Configure(EntityTypeBuilder<ChatNotifications> builder)
        {
            builder.ToTable("chat_notifications");
            builder.HasKey(c => new {c.ChatId, c.NotificationId});

            builder.HasOne(c => c.Chat)
                .WithMany(c => c.ChatNotifications)
                .HasForeignKey(c => c.NotificationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Notification)
                .WithMany(c => c.ChatNotifications)
                .HasForeignKey(c => c.NotificationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
