﻿using System.ComponentModel.DataAnnotations;

namespace WEUPanel.Pages.Account
{
    public class RegisterModel
    {
        [Required]

        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        [Required]

        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Required]

        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


    }
}
