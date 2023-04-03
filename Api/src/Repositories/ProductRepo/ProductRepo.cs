using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.src.Database;
using Api.src.Models;
using Api.src.Repositories.BaseRepo;
using Microsoft.EntityFrameworkCore;

namespace Api.src.Repositories.ProductRepo
{
    public class ProductRepo : BaseRepo<Product>, IProductRepo
    {
        public ProductRepo(DatabaseContext context) : base(context) { }
        public async Task<IEnumerable<Product>> GetAllByCategoryIdAsync(Guid categoryId, QueryOptions options)
        {
            return await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
        }
    }
}