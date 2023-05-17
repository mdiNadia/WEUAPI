using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Account
{
    public class ForgotPasswordModel
    {
        [Required]
        public string EmailOrPhoneNumber { get; set; }


    }
}
