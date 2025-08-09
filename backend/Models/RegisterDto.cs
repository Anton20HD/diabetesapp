using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Firstname is required")]
        [RegularExpression(@"^[A-Za-zÅÄÖåäö\s'-]+$", ErrorMessage = "Name can only contain letters, spaces, apostrophes and hyphens")]
        public string FirstName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Lastname is required")]
        [RegularExpression(@"^[A-Za-zÅÄÖåäö\s'-]+$", ErrorMessage = "Name can only contain letters, spaces, apostrophes and hyphens")]
        public string LastName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid Email")]
        [RegularExpression(@"^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Invalid Email format")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be atleast 6 digits")]
         [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one number.")]
        public string Password { get; set; } = string.Empty;

    }
}