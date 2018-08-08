using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealthWholesaler.Models;
using System.Data.Entity;
using HealthWholesaler.ViewModel;

namespace HealthWholesaler.Controllers
{
    public class ProductController : Controller
    {
        private HealthWholesalerDBEntities _context;

        public ProductController()
        {
            _context = new HealthWholesalerDBEntities();     
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();  
        }

        // GET: Product
        public ViewResult Index()
        {
            var product = _context.Products.Include(p => p.Category).ToList();
            return View(product);
        }

        // code not used
        public ViewResult New()
        {
            var productcategory = _context.Categories.ToList();

            var productcategoryVM = new ProductCategory
            {
                Categories = _context.Categories.ToList()
            };

            return View("New", productcategoryVM);
        }

        // code not used
        [HttpPost]
        public ActionResult Create(ProductCategory viewmodel)
        {
            Product product = viewmodel.Products;

            if (!ModelState.IsValid)
            {
                var validationviewmodel2 = new ProductCategory
                {
                    Products = product,
                    Categories = _context.Categories.ToList()
                };

                return View("New", validationviewmodel2);
            }; 


            if (product.ID == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                var productindb = _context.Products.Single(p => p.ID == product.ID);
                productindb.ID = product.ID;
                productindb.Name = product.Name;
                productindb.Price = product.Price;
                productindb.NumberInStock = product.NumberInStock;
                productindb.CategoryId = product.CategoryId;
            }
            
            _context.SaveChanges();

            return RedirectToAction("Index", "Product");
        }

        // code not used
        public ActionResult Edit(int id)
        {
            var products = _context.Products.SingleOrDefault(p => p.ID == id);
            if (products == null)
                return HttpNotFound();

            var productcategoryVM = new ProductCategory
            {
                Products = products,
                Categories = _context.Categories.ToList()
        };

            return View("New", productcategoryVM);
        }
    }
}
