using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;
using Smart_Utube.Models.Enums;

namespace Smart_Utube.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }

        public Profile? Profile { get; set; }

        public UserRole Role { get; set; } = UserRole.User;

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
        public ICollection<WatchHistory> WatchHistories { get; set; } = new List<WatchHistory>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
