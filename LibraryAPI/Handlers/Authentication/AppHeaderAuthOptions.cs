using Microsoft.AspNetCore.Authentication;

namespace LibraryAPI.Handlers.Authentication
{
    public class AppHeaderAuthOptions : AuthenticationSchemeOptions
    {
        public string[] AllowedNames { get; set; }
    }
}
