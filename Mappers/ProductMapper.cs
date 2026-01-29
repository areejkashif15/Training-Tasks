using System;
using ProductManagement.Models;
using ProductManagement.DTO;

namespace ProductManagement.Mappers
{
    public static class ProductMapper
    {
        public static ProductDTO ToDto(this Product src)
        {
            if (src == null) return null!;
            return new ProductDTO
            {
                Id = src.Id,
                Name = src.Name,
                Description = src.Description,
                Price = src.Price,
                CategoryId = src.CategoryId,
                CategoryName = src.Category?.Name
            };
        }

        public static Product FromCreateDto(this CreateProductDTO dto)
        {
            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId ?? 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public static void ApplyUpdatePrice(this Product entity, UpdateProductPriceDTO dto)
        {
            entity.Price = dto.Price;
            entity.UpdatedAt = DateTime.UtcNow;
        }

        public static ProductWithCategoryDTO ToWithCategoryDto(this Product src)
        {
            if (src == null) return null!;

            return new ProductWithCategoryDTO
            {
                Id = src.Id,
                Name = src.Name,
                Description = src.Description,
                Price = src.Price,
                Category = src.Category == null ? null : new CategoryDTO
                {
                    Id = src.Category.Id,
                    Name = src.Category.Name
                }
            };
        }

    }
}
