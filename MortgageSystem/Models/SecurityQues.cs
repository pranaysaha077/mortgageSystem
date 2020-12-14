using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MortgageSystem.Models
{
    public class SecurityQues
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Enter the Favorite Pet")]
        public string  FQues{get; set;}

        [Required]
        [Display(Name = "Enter Your Contact Number")]
        public string SQues { get; set; }

        [Required]
        [Display(Name = "Enter Your Favorite Car Brand")]
        public string TQues { get; set; }

        public int CustomerId { get; set; }

       

    }
}