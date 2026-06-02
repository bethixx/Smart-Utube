using Microsoft.AspNetCore.Mvc;
using Smart_Utube.Services;
using Smart_Utube.DTOs.Movie;

namespace Smart_Utube.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesApiController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesApiController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieReadDto>>> GetAll(
            string? searchString, int? categoryId)
        {
            var movies = await _movieService.GetAllAsync(searchString, categoryId);
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieReadDto>> GetById(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MovieCreateDto dto)
        {
            await _movieService.CreateAsync(dto);
            return Ok();
        }
    }
}