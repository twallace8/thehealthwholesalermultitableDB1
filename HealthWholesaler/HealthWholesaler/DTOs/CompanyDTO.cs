using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthWholesaler.Models;
using System.ComponentModel.DataAnnotations;

namespace HealthWholesaler.DTOs
{
    public class CompanyDTO
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        //[Required]
        public string Headquarters { get; set; }

        public int DeliveryPackageId { get; set; }

        public DeliveryPackageDTO DeliveryPackage { get; set; }
    }
}