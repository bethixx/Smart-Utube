using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Utube.Data;
using Smart_Utube.Models;

namespace Smart_Utube.Controllers
{
    public class PlaylistMoviesController : Controller
    {
        private readonly AppDbContext _context;

        public PlaylistMoviesController(AppDbContext context)
        {
            _context = context;
        }

        // ADD MOVIE TO PLAYLIST
        [HttpPost]
        public async Task<IActionResult> AddToPlaylist(int movieId, int playlistId)
        {
            var exists = await _context.PlaylistMovies
                .AnyAsync(pm =>
                    pm.MovieId == movieId &&
                    pm.PlaylistId == playlistId);

            if (!exists)
            {
                var playlistMovie = new PlaylistMovie
                {
                    MovieId = movieId,
                    PlaylistId = playlistId
                };

                _context.PlaylistMovies.Add(playlistMovie);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Playlists", new { id = playlistId });
        }

        // REMOVE MOVIE FROM PLAYLIST
        public async Task<IActionResult> RemoveFromPlaylist(int movieId, int playlistId)
        {
            var playlistMovie = await _context.PlaylistMovies
                .FindAsync(playlistId, movieId);

            if (playlistMovie != null)
            {
                _context.PlaylistMovies.Remove(playlistMovie);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Playlists", new { id = playlistId });
        }
    }
}