using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.src.Models;
using Api.src.Repositories.BaseRepo;

namespace Api.src.Repositories.ProductRepo
{
    public interface IProductRepo : IBaseRepo<Product>
    {
        Task<IEnumerable<Product>> GetAllByCategoryIdAsync(Guid categoryId, QueryOptions options);
    }
}