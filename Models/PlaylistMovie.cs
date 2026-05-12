namespace Smart_Utube.Models
{
    public class PlaylistMovie
    {
        public int PlaylistId { get; set; }
        public Playlist Playlist { get; set; } = null!;

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
    }
}