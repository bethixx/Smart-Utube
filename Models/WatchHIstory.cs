using Microsoft.AspNetCore.Identity;

namespace Smart_Utube.Models
{
    public class WatchHistory
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; } = null!;

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public DateTime WatchedAt { get; set; } = DateTime.UtcNow;
    }
}