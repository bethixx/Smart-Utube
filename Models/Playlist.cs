using Microsoft.AspNetCore.Identity;

namespace Smart_Utube.Models
{
    public class Playlist
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; } = null!;

        public required string Name { get; set; }

        public ICollection<PlaylistMovie> PlaylistMovies { get; set; } = new List<PlaylistMovie>();
    }
}