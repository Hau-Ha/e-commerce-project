using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.src.DTOs;
using Api.src.Models;
using Api.src.Repositories.BaseRepo;
using Api.src.Repositories.UserRepo;
using Api.src.Services.BaseService;
using AutoMapper;

namespace Api.src.Services.UserService
{
    public class UserService
        : BaseService<User, UserReadDto, UserCreateDto, UserUpdateDto>,
            IUserService
    {
        public UserService(IMapper mapper, IUserRepo repo)
            : base(mapper, repo) { }

        public override async Task<UserReadDto> CreateOneAsync(UserCreateDto create)
        {
            var entity = _mapper.Map<UserCreateDto, User>(create);
            /*  hash the password before saving into databse */
            var result = await _repo.CreateOneAsync(entity);
            if (result is null)
            {
                throw new Exception("Cannot create");
            }
            return _mapper.Map<User, UserReadDto>(result);
        }
    }
}
