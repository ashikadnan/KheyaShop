using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Data.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required!")]
        public string FullName { get; set; }

        public string Name { get; set; }

        [Display(Name ="Email Address")]
        [Required(ErrorMessage = "Email address is required!")]
        public string EmailAddress { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is required!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match!" )]
        public string ConfirmPassword { get; set; }
    }
}
