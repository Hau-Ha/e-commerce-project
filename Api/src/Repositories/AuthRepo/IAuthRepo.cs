using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.src.DTOs;
using Api.src.Models;
using Microsoft.AspNetCore.Identity;

/*  2 step authentication
1. send email + password --> access token
2. use access token --> user profile */

namespace Api.src.Repositories.AuthRepo
{
    public interface IAuthRepo
    {
        Task<User?> LogInAsync (AuthDto auth);
    }
}