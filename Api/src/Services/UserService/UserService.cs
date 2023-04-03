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

          public override async Task<UserReadDto> CreateOneAsync(UserCreateDto dto)
    {
        ServiceHash.CreateHashData(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);
        var entity = _mapper.Map<UserCreateDto, User>(dto);
        entity.Password = Convert.ToBase64String(passwordHash);
        entity.Salt = passwordSalt;
        await _repo.CreateOneAsync(entity);
        return _mapper.Map<User, UserReadDto>(entity);
    }

        public class ServiceHash
        {
            public static void CreateHashData(string input, out byte[] inputHash, out byte[] inputSalt)
            {
                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    inputSalt = hmac.Key;
                    inputHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
                }
            }

            public static bool CompareHashData(string input, byte[] inputHash, byte[] inputSalt)
            {
                using (var hmac = new System.Security.Cryptography.HMACSHA512(inputSalt))
                {
                    var computedInput = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
                    return computedInput.SequenceEqual(inputHash);
                }
            }
        }
    }
}
