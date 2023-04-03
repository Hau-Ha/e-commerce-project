using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.src.DTOs
{
    public class AuthDto
    {
        public string Email { get; set; } = null!;
       public string Password { get; set; } = null!;
    }
}