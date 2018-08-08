using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthWholesaler.DTOs
{
    public class NewOrderDTO
    {
        public int CompanyId { get; set; }
        public List<int> ProductIds { get; set; }
        public List<int> OfferIds { get; set; }
    }
}