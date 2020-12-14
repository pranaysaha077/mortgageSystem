using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace MortgageSystem.Models
{
    public class Emi
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Emi Number")]
        public int EmiNumber { get; set; }

        public double MonthlyAmt { get; set; }

        [Required]
        [Display(Name ="Amount To Pay")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F3}")]

        public double AmountToPaid { get; set; }

      
        [Display(Name ="Outstanding Balance")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F3}")]

        public double OutstandingBal { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Last Payment Date")]
        public DateTime? LDayOfPayment { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Next Due Date")]
        public DateTime? NDayOfPayment{ get; set; }


        //foriegn key and navigation property
        public string LoanNumber { get; set; }

        //finance manager will 
        //after the approval then finace manager will generate the EMI Tracker table clicking on the link table will autmatically created 
        //and cutomer will be able to view it. and there will a payment column onclinking tht link or button it will redirect payment page
      //10 lac *    
    }
}