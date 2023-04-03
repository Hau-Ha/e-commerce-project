using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.src.DTOs;
using Api.src.Models;
using Api.src.Repositories.BaseRepo;
using Api.src.Repositories.ProductRepo;
using Api.src.Services.BaseService;
using AutoMapper;

namespace Api.src.Services.ProductService
{
    public class ProductService : BaseService<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>, IProductService
    {

        public ProductService(IMapper mapper, IProductRepo repo) : base(mapper, repo) { }
        public async Task<IEnumerable<ProductReadDto>> GetProductsByCategoryIdAsync(Guid categoryId, QueryOptions options)
        {
            var products = await _repo.GetAllAsync(options);
            return _mapper.Map<IEnumerable<ProductReadDto>>(products.Where(p => p.CategoryId == categoryId));
        }
    }
}