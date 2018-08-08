using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthWholesaler.Models;
using System.ComponentModel.DataAnnotations;

namespace HealthWholesaler.DTOs
{
    public class OffersDTO
    {
        public int ID { get; set; }

        public string Picture { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        public string Sale_Price { get; set; }

        [Required]
        [Range(1, 10)]
        public int NumberInStock { get; set; }

        public int NumberAvailable { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}