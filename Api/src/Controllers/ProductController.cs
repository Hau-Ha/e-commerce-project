using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.src.DTOs;
using Api.src.Models;
using Api.src.Services.ProductService;
using Microsoft.AspNetCore.Components;

namespace Api.src.Controllers
{
    public class ProductController : BaseController<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>
    {
        public ProductController(IProductService service) : base(service)
        {
        }
    }
}