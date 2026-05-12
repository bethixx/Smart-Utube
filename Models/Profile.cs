namespace Smart_Utube.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }
        public User User { get; set; } = null!;
    }
}
