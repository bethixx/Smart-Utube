namespace Smart_Utube.DTOs.Comment
{
    public class CommentCreateDto
    {
        public int MovieId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
