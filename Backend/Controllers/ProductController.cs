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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ProductDTO>>> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
                return Ok(products);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductDTO>> GetbyID(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        [HttpGet("category/{categoryId}")]
        [Authorize]
        public async Task<ActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductDTO>> CreateProduct(CreateProductDTO dto)
        {
            var createdProduct = await _productService.CreateProductAsync(dto);
            return CreatedAtAction(nameof(GetbyID), new { id = createdProduct.Id }, createdProduct);

        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProductPrice(int id, UpdateProductPriceDTO dto)
        {
            var updatedProduct = await _productService.UpdateProductPriceAsync(id, dto);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
                return NoContent();
        }
    }
}