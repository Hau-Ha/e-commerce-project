using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.src.DTOs;
using Api.src.Helpers;
using Api.src.Models;
using Api.src.Repositories.AuthRepo;
using Microsoft.IdentityModel.Tokens;

namespace Api.src.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _repo;
        private readonly IConfiguration _config;

        public AuthService(IAuthRepo repo, IConfiguration config) {
            _repo = repo;
            _config = config;
        }

        public async Task<string> LogInAsync(AuthDto auth)
        {
            var user = await _repo.LogInAsync(auth);
            if(user is null)
            {
                throw ServiceException.Unauthorized("Credentials are wrong");
            }
            return CreateToken(user);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var secrete = _config["AppSettings:Token"];
            var tokenHander = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secrete!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddDays(7)
            };
            var token = tokenHander.CreateToken(tokenDescriptor);
            return tokenHander.WriteToken(token);
        }
    }
}
