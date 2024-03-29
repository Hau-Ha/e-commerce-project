using System;
using Api.src.DTOs;
using Api.src.Models;
using Api.src.Repositories.BaseRepo;
using Api.src.Services.BaseService;

namespace Api.src.Services.ProductService
{
    public interface IProductService : IBaseService<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>
    {
        Task<IEnumerable<ProductReadDto>> GetProductsByCategoryIdAsync(Guid categoryId, QueryOptions options);
    }
}