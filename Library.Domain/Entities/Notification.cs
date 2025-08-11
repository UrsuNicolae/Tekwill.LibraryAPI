using Library.Domain.Enums;

namespace Library.Domain.Entities
{
    public class Notification
    {
        public long Id { get; set; }

        public NotificationType NotificationType { get; set; }

        public ICollection<ChatNotifications>? ChatNotifications { get; set; }
    }
}
