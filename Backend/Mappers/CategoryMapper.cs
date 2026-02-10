using System;
using ProductManagement.Models;
using ProductManagement.DTO;

namespace ProductManagement.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDTO ToDto(this Category src)
        {
            if (src == null) return null!;
            return new CategoryDTO
            {
                Id = src.Id,
                Name = src.Name,
                Description = src.Description
            };
        }

        public static Category FromCreateDto(this CreateCategoryDTO dto)
        {
            return new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}
