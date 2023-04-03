using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.src.DTOs;
using Api.src.Models;
using Api.src.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.src.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoryController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [HttpGet("{categoryId}/products")]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetProductsByCategoryId(Guid categoryId)
        {
            var products = await _productService.GetAllByCategoryIdAsync(categoryId);
            return Ok(products);
        }
    }
}
