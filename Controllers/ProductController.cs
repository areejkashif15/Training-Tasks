using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            
            // try{
            //     _logger.LogInformation("Fetching all products");
            //     var products = await _productService.GetAllProductsAsync();
            //     return Ok(products);
            // } 
            // catch(Exception ex){
            //     _logger.LogError(ex, "An error occurred while fetching products");
            //     throw;
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetbyID(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

            // try{
            //     _logger.LogInformation("Fetching product with ID: {ProductID}");
            //     var product = await _productService.GetProductByIdAsync(id);
            //     if (product == null)
            //     {
            //         _logger.LogWarning($"Product with ID: {id} not found");
            //         return NotFound();
            //     }
            //     return Ok(product);
            // } 
            // catch(Exception ex){
            //     _logger.LogError(ex, $"An error occurred while fetching product with ID: {id}");
            //     throw;
            
        }


        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct(CreateProductDTO dto)
        {
            var createdProduct = await _productService.CreateProductAsync(dto);
            return CreatedAtAction(nameof(GetbyID), new { id = createdProduct.Id }, createdProduct);

            // try{
            //     _logger.LogInformation("Creating a new product");
            //     var createdProduct = await _productService.CreateProductAsync(product);
            //     return CreatedAtAction(nameof(GetbyID), new { id = createdProduct.Id }, createdProduct);
            // } 
            // catch(Exception ex){
            //     _logger.LogError(ex, "An error occurred while creating a new product");
            //     throw;
            // }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductPrice(int id, UpdateProductPriceDTO dto)
        {
            var updatedProduct = await _productService.UpdateProductPriceAsync(id, dto);
            return Ok(updatedProduct);

            // try{
            //     _logger.LogInformation($"Updating price for product ID: {id}");
            //     var updatedProduct = await _productService.UpdateProductPriceAsync(id, price);
            //     return Ok(updatedProduct);  
            // }
            // catch(Exception ex){
            //     _logger.LogError(ex, $"An error occurred while updating price for product ID: {id}");
            //     throw;
            // }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
                return NoContent();

            // try{
            //     _logger.LogInformation($"Deleting product with ID: {id}");
            //     await _productService.DeleteProductAsync(id);
            //     return NoContent();
            // }
            // catch(Exception ex){
            //     _logger.LogError(ex, $"An error occurred while deleting product with ID: {id}");
            //     throw;
            // }
        }
    }
}