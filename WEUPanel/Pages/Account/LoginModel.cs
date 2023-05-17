using System.ComponentModel.DataAnnotations;

namespace WEUPanel.Pages.Account
{
    public partial class LoginModel
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }


    }
}
