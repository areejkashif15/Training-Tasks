using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProductManagement.Interface;
using ProductManagement.Models;
using ProductManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace ProductManagement.Repository
{
   
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> CategoryNameExistsAsync(string name, int? excludeId = null)
        {
            var query = _context.Categories.Where(c => c.Name == name);
            if (excludeId.HasValue)
                query = query.Where(c => c.Id != excludeId.Value);
            return await query.AnyAsync();
        }
    }
}