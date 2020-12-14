using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MortgageSystem.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="First Name")]
        [RegularExpression(@"^[a-zA-Z_]+([a-zA-Z_]+)*$", ErrorMessage = "Characters are not allowed")]
        public string FName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z_]+([a-zA-Z_]+)*$", ErrorMessage = "Characters are not allowed")]
        public string LName { get; set; }

        [Required]
        [Display(Name ="User Id")]
        public string UserId { get; set; }


        [Required]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Password should be a minimum of 6 and a maximum of 18 characters along with one upper case, one lower case and one digit")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password{ get; set; }

        [Required(ErrorMessage = "Re-Enter your Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPass { get; set; }

        [Required]
        public string Gender{ get; set;   }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Date of Birth")]
        public DateTime Dob { get; set; }

        [EmailAddress]
       
        [Required]
        public string Email { get; set; }

        [RegularExpression(@"^[6-9]{1}[0-9]{9}$", ErrorMessage = "Please provide with a valid contact number")]
        [Required(ErrorMessage = "Contact Number is required")]
        [Display(Name = "Contact Number")]
        public long Contact { get; set; }

        //foreign key and navigation property
        
        
       // public ICollection<Loan> Loans { get; set; }

        //security Questions 
        [Required]
        [Display(Name ="Favorite Animal")]
        public string SecurityAnimal{ get; set; }

        [Required]
        [Display(Name ="Favorite Number")]
        public string SecurityNumber { get; set; }

        [Required]
        [Display(Name ="Birth Place")]
        public string SecurityBirthPlace { get; set; }


//
    }
}