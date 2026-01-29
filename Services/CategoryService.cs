using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using ProductManagement.DTO;
using ProductManagement.Interface;
using ProductManagement.Models;
using ProductManagement.Mappers;
using ProductManagement.Exceptions;  
using Serilog;     

namespace ProductManagement.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepo categoryRepo, ILogger<CategoryService> logger)
        {
            _categoryRepo = categoryRepo;
            _logger = logger;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            _logger.LogInformation("Fetching all categories from the repository.");
            
            var categories = await _categoryRepo.GetAllCategoriesAsync();
            _logger.LogInformation($"Successfully fetched {categories.Count} all categories.");
            return categories.Select(c => c.ToDto()).ToList();
        
        }

        public async Task<CategoryDTO?> GetCategoryByIdAsync(int id)
        {
            if (id <= 0)
            {
                Log.Warning("Invalid category ID provided | CategoryId: {CategoryId}", id);
                throw new BadRequestException("Category ID must be greater than 0", nameof(id));
            }
            _logger.LogInformation($"Fetching category | Category with ID: {id} from the repository.");
            
            var category = await _categoryRepo.GetCategoryByIdAsync(id);
            if (category == null){
                Log.Warning("Category not found | CategoryId: {CategoryId}", id);
                throw new NotFoundException("Category", id);
            }
            Log.Information("✅ Category found | CategoryId: {CategoryId} | Name: {CategoryName}", category.Id, category.Name);
            return category.ToDto();
            
        }

        public async Task<CategoryDTO> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            _logger.LogInformation("Creating a new category | Name: {CategoryName}", createCategoryDTO.Name);
            
            bool nameExists = await _categoryRepo.CategoryNameExistsAsync(createCategoryDTO.Name);
            if (nameExists)
            {
                Log.Warning("⚠️ Category name already exists | Name: {CategoryName}", createCategoryDTO.Name);
                throw new ConflictException($"Category with name '{createCategoryDTO.Name}' already exists");
            }

            var category = createCategoryDTO.FromCreateDto();
            var newCategory = await _categoryRepo.CreateCategoryAsync(category);

            Log.Information("✅ Category created successfully | CategoryId: {CategoryId} | Name: {CategoryName}", newCategory.Id, newCategory.Name);
            return newCategory.ToDto();
        }
     
    }
}