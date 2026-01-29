using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProductManagement.DTO;
using ProductManagement.Models;

namespace ProductManagement.Interface
{
    public interface IProductRepo
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<Product> UpdateProductPriceAsync(int id, decimal price);
        Task DeleteProductAsync(int id);
        
    }
}