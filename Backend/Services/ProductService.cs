using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using ProductManagement.DTO;
using ProductManagement.Interface;
using ProductManagement.Models;
using ProductManagement.Mappers;
using ProductManagement.Repository;
using ProductManagement.Exceptions;  
using Serilog;     

namespace ProductManagement.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepo productRepo, ICategoryRepo categoryRepo, ILogger<ProductService> logger)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _logger = logger;
        }

        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            _logger.LogInformation("Fetching all products from the repository.");
            
            var products = await _productRepo.GetAllProductsAsync();
            Log.Information("✅ Successfully fetched {Count} products", products.Count);
            
            return products.Select(p => p.ToDto()).ToList();

        }

        public async Task<ProductDTO?> GetProductByIdAsync(int id)
        {
            if (id <= 0)
            {
                Log.Warning("Invalid product ID provided | ProductId: {ProductID}", id);
                throw new BadRequestException("Product ID must be greater than 0", nameof(id));
            }
            _logger.LogInformation($"Fetching product | Product with ID: {id} from the repository.");
        
            var product = await _productRepo.GetProductByIdAsync(id);
            if (product == null){
                Log.Warning("Product not found | ProductId: {ProductID}", id);
                throw new NotFoundException("Product", id);
            }
            Log.Information("✅ Product found | ProductId: {ProductID} | Name: {ProductName}", product.Id, product.Name);
            return product.ToDto();
        }
        public async Task<List<ProductWithCategoryDTO>> GetProductsByCategoryAsync(int categoryId)
        {
            if (categoryId <= 0){
                Log.Warning("Invalid category ID provided | CategoryId: {CategoryId}", categoryId);
                throw new BadRequestException("Category ID must be greater than 0", nameof(categoryId));
            }

            var products = await _productRepo.GetProductsByCategoryIdAsync(categoryId);

            if (products.Count == 0){
                Log.Information("No products found for CategoryId: {CategoryId}", categoryId);
                return new List<ProductWithCategoryDTO>();
            }

            Log.Information("✅ Successfully fetched {Count} products for CategoryId: {CategoryId}", products.Count, categoryId);
            return products.Select(p => p.ToWithCategoryDto()).ToList();
        }
        public async Task<ProductDTO> CreateProductAsync(CreateProductDTO createProductDTO)
        {           
            _logger.LogInformation("Creating a new product | Name: {ProductName}", createProductDTO.Name);

            if (createProductDTO.CategoryId.HasValue) {
                var category = await _categoryRepo.GetCategoryByIdAsync(createProductDTO.CategoryId.Value);
                if (category == null){
                    Log.Warning("Category does not exist | CategoryId: {CategoryId}", createProductDTO.CategoryId);
                    throw new NotFoundException("Category", createProductDTO.CategoryId ?? 0);
                }
            }

            if (createProductDTO.Price <= 0 || createProductDTO.Price > 1000){
                Log.Warning("Invalid price provided | Price: {Price}", createProductDTO.Price);
                throw new BadRequestException("Price must be greater than zero and less than thousand.", nameof(createProductDTO.Price));
            }

            if (decimal.Round(createProductDTO.Price, 2) != createProductDTO.Price){
                Log.Warning("Invalid price precision | Price: {Price}", createProductDTO.Price);
                throw new BadRequestException("Price can have at most two decimal places.");
            }
            
            var product = createProductDTO.FromCreateDto();
            var newProduct = await _productRepo.AddProductAsync(product);

            Log.Information("✅ Product created successfully | ProductId: {ProductId} | Name: {ProductName}", newProduct.Id, newProduct.Name);

            return newProduct.ToDto();
           
        }

        public async Task<ProductDTO> UpdateProductPriceAsync(int id, UpdateProductPriceDTO updateProductPriceDTO)
        {
            Log.Information("Updating price for ProductId: {ProductID} to {NewPrice}", id, updateProductPriceDTO.Price);
            //id > 0 price > 0 
            if (id <= 0){
                Log.Warning("Invalid product ID provided | ProductId: {ProductID}", id);
                throw new BadRequestException("Product ID must be greater than 0", nameof(id));
            }

            if (updateProductPriceDTO.Price <= 0 || updateProductPriceDTO.Price > 1000){
                Log.Warning("Invalid price provided | Price: {Price}", updateProductPriceDTO.Price);
                throw new BadRequestException("Price must be greater than zero and less than thousand.", nameof(updateProductPriceDTO.Price));
            }

            if (decimal.Round(updateProductPriceDTO.Price, 2) != updateProductPriceDTO.Price){
                Log.Warning("Invalid price precision | Price: {Price}", updateProductPriceDTO.Price);
                throw new BadRequestException("Price can have at most two decimal places.");
            }

            var product = await _productRepo.UpdateProductPriceAsync(id, updateProductPriceDTO.Price);
            // if (product == null){
            //     throw new KeyNotFoundException("Product not found.");
            //     return null;
            // }

            Log.Information("Price updated successfully for ProductId: {ProductID} | New Price: {NewPrice}", product.Id, product.Price);
            return product.ToDto();
            
        }

        
        public async Task DeleteProductAsync(int id)
        {
            if (id <= 0)
            {
                Log.Warning("Invalid product ID provided | ProductId: {ProductID}", id);
                throw new BadRequestException("Product ID must be greater than 0", nameof(id));
            }

            Log.Information("Deleting product | ProductId: {ProductID}", id);
            await _productRepo.DeleteProductAsync(id);
        }
    }
}