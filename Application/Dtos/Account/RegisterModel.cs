using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Register
{
    public class RegisterModel
    {
        //[Required]
        public string? FirstName { get; set; } 
        //[Required]
        public string? LastName { get; set; } 
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NumCode { get; set; }
        public string? Role { get; set; } 
        public string Code { get; set; }
    }
}
