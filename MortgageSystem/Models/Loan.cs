using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MortgageSystem.Models
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public int Tenure { get; set; }

       
        [Required]
        [Display(Name = "Interest Rate")]
        public  double InterestRate { get; set; }

        [Required]
        [Display(Name = "Bank Statement")]
        public string BankStatement { get; set; }

        [Required]
        [Display(Name = "Financial Status")]
        public string FinancialStatus { get; set; }

        [Required]
        [Display(Name = "Loan Status")]
        public string LoanStatus { get; set; }

        //foreign key and navigation property

      
        public int CustomerId { get; set; }
       
        

       



    }
}