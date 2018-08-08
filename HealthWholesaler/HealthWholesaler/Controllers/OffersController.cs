using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealthWholesaler.Models;
using HealthWholesaler.ViewModel;
using System.Data.Entity;
using System.Data.Entity.Validation;



namespace HealthWholesaler.Controllers
{
    public class OffersController : Controller
    {
        private HealthWholesalerDBEntities _context;

        // Creating constructor for access to the Database
        public OffersController()
        {
            _context = new HealthWholesalerDBEntities();     
        }

        // Overriding the Dispose Method
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Offers
        public ViewResult Index(string searching)
        {
            // LINQ Query to select offer from offers
            var selectoffers = from o in _context.Offers
                               select o;

            // test if searchstring passed is null or empty
            if (!String.IsNullOrEmpty(searching))
            {
                // if it's not take offers var and assign to it result of the LINQ query
                selectoffers = selectoffers.Where(i => i.Name.Contains(searching));
            }

            // return offers found from the search
            return View(selectoffers); 

            // Requesting the list of offers
            var offers = _context.Offers.Include(o => o.Category).ToList();

            return View(offers);
        }

        // Action for Adding Offer Page
        public ActionResult AddOffer()
        {
            var categories = _context.Categories.ToList();
            var categoryVM = new OffersCategory
            {
                Category = categories
            };
            return View("AddOffer", categoryVM);
        }

        // Action for Offers Edit Page
        public ActionResult Edit(int id)
        {
            var offers = _context.Offers.SingleOrDefault(o => o.ID == id);
            if (offers == null)
                return HttpNotFound();

            var OffersCategoryVM = new OffersCategory
            {
                Offer = offers,
                Category = _context.Categories.ToList()
            };

            return View("New", OffersCategoryVM);
        }
        

        // Creating the new offer/product, using model binding 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOffer(Offer offer)
        {
            // Provides a default image url in the text box, in case the user does not enter one
            if (offer.Picture == null)
            {
                offer.Picture = "https://5.imimg.com/data5/GV/DP/MY-3831378/500ml-plastic-water-bottle-500x500.jpg";
            }

            // second part of validation
            if (!ModelState.IsValid)
            {
                var validationviewmodel = new OffersCategory
                {
                    Category = _context.Categories.ToList(),
                    Offer = offer
                };

                return View("AddOffer", validationviewmodel);
            }

            _context.Offers.Add(offer);

            _context.SaveChanges();

            return RedirectToAction("Index", "Offers");
        }

        // Editing the new offer/product
        [HttpPost]
       [ValidateAntiForgeryToken]
        public ActionResult Create(Offer offer)
        {
            // Provides a default image url in the text box, in case the user does not enter one
            if (offer.Picture == null)
            {
                offer.Picture = "https://5.imimg.com/data5/GV/DP/MY-3831378/500ml-plastic-water-bottle-500x500.jpg";
            }

            // second part of validation
            if (!ModelState.IsValid)
            {
                var validationviewmodel = new OffersCategory
                {
                    Category = _context.Categories.ToList(),
                    Offer = offer
                };

                return View("New", validationviewmodel);
            }

            var offerindb = _context.Offers.Single(o => o.ID == offer.ID);
            offerindb.ID = offer.ID;
            offerindb.Name = offer.Name;
            offerindb.Sale_Price = offer.Sale_Price;
            offerindb.NumberInStock = offer.NumberInStock;
            offerindb.Picture = offer.Picture; 
            offerindb.CategoryId = offer.CategoryId;

            _context.SaveChanges();

            return RedirectToAction("Index", "Offers");
        }




    }
}