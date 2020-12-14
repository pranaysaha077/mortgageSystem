using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc.Html;

namespace MortgageSystem.Models
{
    public class Terms
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Display(Name="Terms & Conditions")]
        [DataType(DataType.MultilineText)]
        public string Term{ get; set; }
    }
}