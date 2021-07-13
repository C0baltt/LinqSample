using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication4.Models
{
    public class TransferViewModel
    {
        public SelectListItem[] Accounts { get; set; }

        [Required]
        [Range(1, 1000000)]
        public decimal Amount { get; set; }

        [Required]
        public Guid? From { get; set; }

        [Required]
        public Guid? To { get; set; }

        public string CurrencyCharCode { get; set; }

        public SelectListItem[] AvailableCharCodes { get; set; }
    }
}
