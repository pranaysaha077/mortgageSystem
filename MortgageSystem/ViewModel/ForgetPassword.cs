using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MortgageSystem.ViewModel
{
    public class ForgetPassword
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Favorite Animal")]
        public string SecurityAnimal { get; set; }

        [Required]
        [Display(Name = "Favorite Number")]
        public string SecurityNumber { get; set; }

        [Required]
        [Display(Name = "Birth Place")]
        public string SecurityBirthPlace { get; set; }

    }
}