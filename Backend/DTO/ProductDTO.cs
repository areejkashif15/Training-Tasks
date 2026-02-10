using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }

    public class CreateProductDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; } 
        public int? CategoryId { get; set; }
    }

    public class ProductWithCategoryDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; } 
        public CategoryDTO? Category { get; set; }
    }

    public class UpdateProductPriceDTO
    {
        public required decimal Price { get; set; } 
    }
}