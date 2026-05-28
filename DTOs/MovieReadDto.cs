namespace Smart_Utube.DTOs.Movie
{
    public class MovieReadDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public required string YouTubeUrl { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
        public DateTime CreatedAt { get; set; }
        public double AverageRating { get; set; }
    }
}
