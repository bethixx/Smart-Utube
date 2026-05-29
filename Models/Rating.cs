using Microsoft.AspNetCore.Identity;

namespace Smart_Utube.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; } = null!;

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public int Value { get; set; }
    }
}
