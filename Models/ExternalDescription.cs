namespace Smart_Utube.Models
{
    public class ExternalDescription
    {
        public int Id { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public string? Source { get; set; }
        public required string GeneratedText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
