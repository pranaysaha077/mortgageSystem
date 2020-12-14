using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MortgageSystem.ViewModel
{
    public class CustomerLoanMortage
    {
        //Customer
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z_]+([a-zA-Z_]+)*$", ErrorMessage = "Characters are not allowed")]
        public string FName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z_]+([a-zA-Z_]+)*$", ErrorMessage = "Characters are not allowed")]
        public string LName { get; set; }
        
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Phone Number")]
        public long Contact { get; set; }        

        [EmailAddress]
        [Required]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required]
        [Display(Name ="Project Plan")]
        public string ProjectPlan { get; set; }


        [Required]
        [Display(Name = "Personal Credit Report")]
        public string PersonalCreditReport { get; set; }

        [Required]
        [Display(Name = "Bussiness Credit Report")]
        public string BussinessCreditReport { get; set; }

        [Required]
        [Display(Name = "Financial Statement")]
        public string FinancialStatement { get; set; }


        [Required]
        [Display(Name = "Bank Statement")]
        public string BankStatement { get; set; }

        //Loan info starts here 
        public string LoanNumber { get; set; }


        [Required]
        public double Amount { get; set; }

        [Required]
        [Display(Name = "LoanTenure")]
        public int LoanTenure { get; set; }


        [Required]
        [Display(Name = "Interest Rate")]
        public double InterestRate { get; set; }

        [Required]
        [Display(Name = "Emi Option")]
        public string EmiOption { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Day Of Emi Payment")]
        public DateTime DayOfEmiPayment { get; set; }
        

       //Fields to be autoFilled
        [Display(Name = "Loan Status")]
        public string LoanStatus { get; set; }

        [Display(Name =("Loan Apply Date"))]
        [DataType(DataType.Date)]
        public DateTime? LoanApplyDate { get; set; }

        //mortgage

        [Required]
        [Display(Name = "Mortgage Item")]
        public string MortgageItem { get; set; }

        [Required]
        [Display(Name = "Value Of Mortgage Item")]
        public double ValueOfMortgage { get; set; }

        

       


    }
}