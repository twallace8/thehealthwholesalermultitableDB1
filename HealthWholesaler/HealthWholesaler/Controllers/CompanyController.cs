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
    public class CompanyController : Controller
    {
        private HealthWholesalerDBEntities _context;

        // Creating constructor for access to the Database 
        public CompanyController()
        {
            _context = new HealthWholesalerDBEntities();      
        }

        // Overriding the Dispose Method
        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); 
        }


        // GET: Company
        public ViewResult Index(string search)
        {
            //code for creating searching
            // LINQ Query to select company from companies
            var companies = from c in _context.Companies
                            select c;

            // test if searchstring passed is null or empty
            if (!String.IsNullOrEmpty(search))
            {
                // if it's not take company var and assign to it result of the LINQ query
                companies = companies.Where(n => n.Name.Contains(search));
            }

            return View(companies);

            // Requesting the list of companies
            var company = _context.Companies.Include(c => c.DeliveryPackage).ToList(); 

            return View(company);
        }

    //    // Action to the Details View: renders company names
    //    public ActionResult Details(int id)
    //{
    //        var company = _context.Companies.SingleOrDefault(c => c.ID == id);

    //        if (company == null)
    //            return HttpNotFound();

    //        return View(company);
    //    }

        // Action to Create Form
        public ViewResult New()
        {
            var deliverymethod = _context.DeliveryPackages.ToList();
            var companydeliveryVM = new CompanyDeliveryPackageVM
            {
                DeliveryPackage = deliverymethod
            }; 

            return View("AddCompany", companydeliveryVM);
        }

        // Creating Company
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCompany(Company company)
        {
            // second part of validation
            if (!ModelState.IsValid)
            {
                var viewmodel = new CompanyDeliveryPackageVM
                {
                    Company = company,
                    DeliveryPackage = _context.DeliveryPackages.ToList()
                };

                return View("AddCompany", viewmodel);
            }

           
                _context.Companies.Add(company);

            _context.SaveChanges();

            return RedirectToAction("Index", "Company");
        }

        // Editing Company
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyForm(Company company)
        {
            // second part of validation
            if (!ModelState.IsValid)
            {
                var viewmodel = new CompanyDeliveryPackageVM
                {
                    Company = company,
                    DeliveryPackage = _context.DeliveryPackages.ToList()
                };

                return View("CompanyForm", viewmodel);
            }

            
                var companyindb = _context.Companies.Single(c => c.ID == company.ID);
                companyindb.ID = company.ID;
                companyindb.Name = company.Name;
                companyindb.DeliveryPackageId = company.DeliveryPackageId;
                companyindb.Headquarters = company.Headquarters;
            

            _context.SaveChanges();

            return RedirectToAction("Index", "Company");
        }


        // Action to the edit page of company 
        public ActionResult Edit(int id)
        {
            var editcompany = _context.Companies.SingleOrDefault(c => c.ID == id);
            if (editcompany == null)
                return HttpNotFound();

            var customerdeliveryVM = new CompanyDeliveryPackageVM
            {
                Company = editcompany,
                DeliveryPackage = _context.DeliveryPackages.ToList()
            };

            return View("CompanyForm", customerdeliveryVM);
        }


    }
} 
