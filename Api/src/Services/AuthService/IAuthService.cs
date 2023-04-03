using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.src.DTOs;

namespace Api.src.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> LogInAsync(AuthDto auth);
    }
}