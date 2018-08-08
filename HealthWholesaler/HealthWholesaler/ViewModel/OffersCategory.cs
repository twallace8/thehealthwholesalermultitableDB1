using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthWholesaler.Models;

namespace HealthWholesaler.ViewModel
{
    public class OffersCategory
    {
        public Offer Offer { get; set; }
        public List<Category> Category { get; set; }
        public string Title
        {
            get
            {
                if (Offer != null && Offer.ID != 0)
                    return "Edit Offer";

                return "New Offer";
            }
        }
    }
}