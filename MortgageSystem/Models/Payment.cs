using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace MortgageSystem.Models
{
    public class Payment
    {
        [Key]
       public int Id { get; set; }

        [Display(Name ="Amount Paid")]
        public double PaidAmount { get; set; }

        [Display(Name ="Date of Payment")]
        public DateTime? DayOfPayemt { get; set; }

        public int EmiId { get; set; }
     
    }
}