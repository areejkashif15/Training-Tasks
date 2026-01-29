using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProductManagement.DTO;

namespace ProductManagement.Interface
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO?> GetProductByIdAsync(int id);
        Task<List<ProductWithCategoryDTO>> GetProductsByCategoryAsync(int categoryId);
        Task<ProductDTO> CreateProductAsync(CreateProductDTO createProductDTO);
        Task<ProductDTO> UpdateProductPriceAsync(int id, UpdateProductPriceDTO updateProductPriceDTO);
        Task DeleteProductAsync(int id);
    }
}