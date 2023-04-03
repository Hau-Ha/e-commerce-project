using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.src.Database;
using Api.src.Models;
using Api.src.Repositories.BaseRepo;

namespace Api.src.Repositories.ProductRepo
{
    public class CategoryRepo : BaseRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(DatabaseContext context) : base(context) { }
    }
}