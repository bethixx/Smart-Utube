using Smart_Utube.DTOs.Category;
using Smart_Utube.Models;
using Smart_Utube.Repositories;

namespace Smart_Utube.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CategoryReadDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();

            return categories.Select(c => new CategoryReadDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public async Task<CategoryReadDto?> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
                return null;

            return new CategoryReadDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task CreateAsync(CategoryCreateDto dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            await _repository.AddAsync(category);
        }

        public async Task UpdateAsync(CategoryUpdateDto dto)
        {
            var category = await _repository.GetByIdAsync(dto.Id);

            if (category == null)
                return;

            category.Name = dto.Name;

            await _repository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}