using System;
using Api.src.DTOs;
using Api.src.Models;
using Api.src.Services.BaseService;

namespace Api.src.Services.ProductService
{
  public interface ICategoryService : IBaseService<Category, CategoryReadDTO, CategoryCreateDTO, CategoryUpdateDTO>
{
}
}