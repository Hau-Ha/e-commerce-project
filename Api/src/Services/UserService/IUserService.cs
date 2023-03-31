using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.src.DTOs;
using Api.src.Models;
using Api.src.Services.BaseService;

namespace Api.src.Services.UserService
{
    public interface IUserService : IBaseService<User, UserReadDto, UserCreateDto, UserUpdateDto>
    {
        
    }
}