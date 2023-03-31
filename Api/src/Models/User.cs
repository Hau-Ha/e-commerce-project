using System.ComponentModel.DataAnnotations;

namespace Api.src.Models
{
    public class User : BaseModel
    {
        public string UserName { get; set; }
        [EmailAddressAttribute]
        public string Email { get; set; }
        public Role Role { get; set; }
        public string Password { get; set; }
        // public byte[] Salt { get; set; }
    }

    public enum Role
    {
        Admin,
        Customer
    }
}