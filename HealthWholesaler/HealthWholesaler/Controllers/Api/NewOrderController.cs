using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthWholesaler.Models;
using HealthWholesaler.DTOs; 

namespace HealthWholesaler.Controllers.Api
{
    public class NewOrderController : ApiController
    {
        HealthWholesalerDBEntities _context;

        public NewOrderController()
        {
            _context = new HealthWholesalerDBEntities();     
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); 
        }

        [HttpPost]
        public IHttpActionResult CreateNewOrder (NewOrderDTO neworder)
        {
            // we don't have any products in the dto
            if (neworder.OfferIds.Count == 0)
                return BadRequest("No Products Ids have been provided");

            var company = _context.Companies.Single(c => c.ID == neworder.CompanyId);
            // if company id is not valid
            if (company == null)
                return BadRequest("CompanyId is not Valid");

            var offers = _context.Offers.Where(p => neworder.OfferIds.Contains(p.ID)).ToList();

            foreach(var i in offers)
            {
                if (i.NumberAvailable == 0)
                    return BadRequest("This product is unavailable"); 
                i.NumberAvailable--; 

                var order = new Order
                {
                    Company = company,
                    Offer = i,
                    Date_Ordered = DateTime.Now
                };

                _context.Orders.Add(order); 
            }

            _context.SaveChanges();

            return Ok(); 
              
        }
    }
}
