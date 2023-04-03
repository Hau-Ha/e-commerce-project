using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.src.DTOs;
using Api.src.Models;
using AutoMapper;

namespace Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            /* CreateMap<TCreateDto, T>(); */
            CreateMap<User, UserReadDto>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<UserCreateDto, User>();
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Category, CategoryReadDTO>();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<CategoryUpdateDTO, Category>();
        }
    }
}