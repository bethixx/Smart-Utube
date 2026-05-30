using System.ComponentModel.DataAnnotations;

namespace Smart_Utube.DTOs.Movie
{
    public class MovieCreateDto
    {
        public string? Title { get; set; }
        [Required]
        [Url(ErrorMessage = "There has to be a YouTube link")]
        [RegularExpression(@"^(https?\:\/\/)?(www\.youtube\.com|youtu\.be)\/.+$",
        ErrorMessage = "There has to be a YouTube link")]
        public required string YouTubeUrl { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
