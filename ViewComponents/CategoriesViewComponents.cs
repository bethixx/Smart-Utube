using Microsoft.AspNetCore.Mvc;
using Smart_Utube.Data;

namespace Smart_Utube.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public CategoriesViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _context.Categories.ToList();

            return View(categories);
        }
    }
}