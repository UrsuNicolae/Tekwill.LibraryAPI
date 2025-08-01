namespace Library.Infrastructure.Configurations
{
    public class GoogleConfigurations
    {
        public static string SectionName => nameof(GoogleConfigurations);

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public string BaseUrl { get; set; }

        public string RedirectPath { get; set; }
    }
}
