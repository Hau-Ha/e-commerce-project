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
   public class CategoryService : BaseService<Category, CategoryReadDTO, CategoryCreateDTO, CategoryUpdateDTO>, ICategoryService
{
    public CategoryService(IMapper mapper, ICategoryRepo repo) : base(mapper, repo)
    {
    }
}
}