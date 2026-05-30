namespace Smart_Utube.DTOs.Movie
{
    public class CommentReadDto
    {
        public int Id { get; set; }

        public string Content { get; set; } = "";

        public DateTime CreatedAt { get; set; }

        public string? UserNickname { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}