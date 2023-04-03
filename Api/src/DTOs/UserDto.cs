using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.src.DTOs
{
    public class UserBaseDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Avatar { get; set; } = null!;
    }

    public class UserReadDto : UserBaseDto { }

    public class UserCreateDto : UserBaseDto
    {
        public string Password { get; set; } = null!;
    }

    public class UserUpdateDto : UserBaseDto
    {
        public string Password { get; set; } = null!;
    }
}
