namespace Library.Domain.Entities
{
    public class Chat
    {
        public long Id { get; set; }

        public string? FirtName { get; set; }
        public string? LastName { get; set; } 

        public string? UserName { get; set; }

        public bool IsForm { get; set; }

        public string? Type { get; set; }
        
        public ICollection<ChatNotifications>? ChatNotifications { get; set; }
    }
}
