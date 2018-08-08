using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthWholesaler.Models;

namespace HealthWholesaler.ViewModel
{
    public class CompanyDeliveryPackageVM
    {
        public List<DeliveryPackage> DeliveryPackage { get; set; }
        public Company Company { get; set; }
        public string Title
        {
            get
            {
                if (Company != null && Company.ID != 0)
                    return "Edit Company";

                return "New Company";
            }
        }

    }
}




