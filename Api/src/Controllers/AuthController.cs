using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.src.DTOs;
using Api.src.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace Api.src.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }
        [HttpPost()]
        public async Task<string> LogInAsync([FromBody]AuthDto auth)
        {
            Console.WriteLine(auth.Email);
            return await _service.LogInAsync(auth);
        }
    }
}