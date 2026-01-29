using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProductManagement.DTO;
using ProductManagement.Models;

namespace ProductManagement.Interface
{
    public interface ICategoryRepo
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<Category> CreateCategoryAsync(Category category);
        Task<bool> CategoryNameExistsAsync(string name, int? excludeId = null);
    }
}