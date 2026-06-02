using System.ComponentModel.DataAnnotations;

namespace Smart_Utube.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public required string YouTubeUrl { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatedAt { get; set; } = DateTime.Today;

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<MovieCategory> MovieCategories { get; set; } = new List<MovieCategory>();
        public ICollection<PlaylistMovie> PlaylistMovies { get; set; } = new List<PlaylistMovie>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<WatchHistory> WatchHistories { get; set; } = new List<WatchHistory>();
    }
}
