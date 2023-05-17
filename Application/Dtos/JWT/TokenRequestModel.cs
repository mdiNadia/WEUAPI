using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.JWT
{
    public class TokenRequestModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
