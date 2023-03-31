using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.src.DTOs;
using Api.src.Models;
using Api.src.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace Api.src.Controllers
{
   
    public class UserController : BaseController<User, UserReadDto, UserCreateDto, UserUpdateDto> 
    {
        public UserController(IUserService service) : base(service)
        {

        }
    }
}