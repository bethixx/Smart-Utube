using Microsoft.AspNetCore.Identity;

namespace Smart_Utube.Models
{
    public class AppUser : IdentityUser
    {
        public string? Nickname { get; set; }
        public string? Bio { get; set; }
        public string? AvatarPath { get; set; }
    }
}
