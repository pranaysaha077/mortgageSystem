using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MortgageSystem.Models
{
    public class CardDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name =("Card No"))]
        public long CardNo { get; set; }

        [Required]
        [Display(Name = ("Card Holder Name"))]
        public string CardHolderName{get; set;}

        [Required]
        [Display(Name = ("Expiry Date"))]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ExpiryDate { get; set; }

        [Required]
        public int CVV { get; set; }

        [Required]
        [Display(Name = ("Card Type"))]
        public string CardType { get; set; }


        public double Balance { get; set; }

        public int? CustomerId { get; set; }
    }
}