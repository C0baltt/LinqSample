using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication4.Models
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }

        public string CurrencyName { get; set; }

        [Required]
        public string CurrencyCharCode { get; set; }

        [Required]
        [Range(1, 1000000)]
        public decimal Amount { get; set; }

        public SelectListItem[] AvailableCharCodes { get; set; }
    }
}
