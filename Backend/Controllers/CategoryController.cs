using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ProductManagement.Data;
using ProductManagement.Models;
using ProductManagement.Interface;
using ProductManagement.DTO;
using ProductManagement.Mappers;
using ProductManagement.Services;

namespace ProductManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<CategoryDTO>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CreateCategoryDTO dto)
        {
            var createdCategory = await _categoryService.CreateCategoryAsync(dto);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
        }
    }
}