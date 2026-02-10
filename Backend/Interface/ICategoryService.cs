using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProductManagement.DTO;

namespace ProductManagement.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO?> GetCategoryByIdAsync(int id);
        Task<CategoryDTO> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO);
    }
}