using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MortgageSystem.ViewModel
{
    public class LoanStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; }
    }
}