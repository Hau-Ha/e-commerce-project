using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.src.DTOs
{
    public class UserBaseDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public class UserReadDto : UserBaseDto { }

    public class UserCreateDto : UserBaseDto
    {
        public string Password { get; set; }
    }

    public class UserUpdateDto : UserBaseDto
    {
        public string Password { get; set; }
    }
}
