using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Utube.Data;
using Smart_Utube.Models;
using System.Diagnostics;


namespace Smart_Utube.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IQuoteService _quoteService;
        private readonly WeatherService _weatherService;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IQuoteService quoteService, WeatherService weatherService)
        {
            _logger = logger;
            _context = context;
            _quoteService = quoteService;
            _weatherService = weatherService;
        }

        public async Task<IActionResult> Index(string searchString, int? categoryId)
        {
            var movies = await _context.Movies
                .Include(m => m.MovieCategories)
                .ThenInclude(mc => mc.Category)
                .ToListAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies
                    .Where(m => m.Title.Contains(searchString))
                    .ToList();
            }

            if (categoryId.HasValue)
            {
                movies = movies
                    .Where(m => m.MovieCategories.Any(mc => mc.CategoryId == categoryId))
                    .ToList();
            }

            var quote = await _quoteService.GetRandomQuoteAsync();
            var temp = await _weatherService.GetTemperatureAsync();

            ViewBag.Quote = quote;
            ViewBag.Temp = temp;

            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
