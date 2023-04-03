using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Api.src.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; }= null!;
        [EmailAddressAttribute]
        public string Email { get; set; } = null!;
        public Role Role { get; set; }
       public string Password { get; set;} = null!; 
       public byte[] Salt { get; set; } = null!;

       public string Avatar { get; set; } = null!;
    }

    public enum Role {
        Admin,
        Customer
    }
}