using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.src.DTOs;
using Api.src.Models;
using Api.src.Repositories.BaseRepo;
using Api.src.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.src.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController :  BaseController<Category, CategoryReadDTO, CategoryCreateDTO, CategoryUpdateDTO>
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoryController(ICategoryService categoryService, IProductService productService) : base(categoryService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [HttpGet("{categoryId}/products")]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetProductsByCategoryId(Guid categoryId, QueryOptions options )
        {
            var products = await _productService.GetProductsByCategoryIdAsync(categoryId, options);
            return Ok(products);
        }
    }
}
