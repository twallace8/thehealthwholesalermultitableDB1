using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthWholesaler.Models;

namespace HealthWholesaler.ViewModel
{
    public class ProductCategory
    {
        public Product Products { get; set; }
        public List<Category> Categories { get; set; }
        public string Title
        {
            get
            {
                if (Products != null && Products.ID != 0)
                    return "Edit Product";

                return "New Product";
            }
        }
    }
}