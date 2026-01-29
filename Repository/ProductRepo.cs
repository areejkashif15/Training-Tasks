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
    public class ProductRepo : IProductRepo
    {
        private readonly ApplicationDbContext _context;

        public ProductRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync(); 
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);   
            //Include -> Eager load related data (prevents N+1 queries)
            //FirstOrDefaultAsync -> returns the first element of a sequence, or a default value if no element is found
        }   

        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.Products.Where(p => p.CategoryId == categoryId).Include(p => p.Category).ToListAsync();
            //Where -> filters the results at the database level so that only products matching the provided categoryId are returned.
        }

        public async Task<Product> UpdateProductPriceAsync(int id, decimal price)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            // FindAsync -> searches the cache and then context for an entity with the given primary key values.
            if (existingProduct == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            existingProduct.Price = price;
            existingProduct.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return existingProduct;
        
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

    }
}