using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthWholesaler.DTOs
{
    public class ProductDTO
    {
        public int ID { get; set; }
        public string Picture { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        public string Price { get; set; }

        [Required]
        [Range(1, 10)]
        public int NumberInStock { get; set; }

        public int NumberAvailable { get; set; }

        public int CategoryId { get; set; }
    }
}