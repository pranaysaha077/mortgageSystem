using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MortgageSystem.ViewModel
{
    public class ForgetUseId
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Email")]
        public string EmailId { get; set; }

        [Required]
        public long Contact { get; set; }
    }
}