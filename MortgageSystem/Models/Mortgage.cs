using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MortgageSystem.Models
{
    public class Mortgage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Mortgage Item")]
        public string MortgageItem { get; set; }
    
        [Required]
        [Display(Name ="Mortgage Value")]
        public double MortgageValue { get; set; }

        
        [Display(Name ="Value Type")]
        public string ValueType { get; set; }

        [Required]
        [Display(Name ="Mortgage's Interest")]
        public double MortgageInterest { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Updated Date")]
        public DateTime UpdatedDate { get; set; }
        
        public int LoanId { get; set; }
       
    }
}