using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MortgageSystem.Models
{
    public class ResetPassword
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="User Id")]
        public string UserId { get; set; }

        [Required]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Password should be a minimum of 6 and a maximum of 18 characters along with one upper case, one lower case and one digit")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Re-Enter your Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPass { get; set; }

        
    }
}