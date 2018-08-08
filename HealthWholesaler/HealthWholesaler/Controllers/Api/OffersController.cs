using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthWholesaler.Models;
using HealthWholesaler.DTOs;
using AutoMapper;


namespace HealthWholesaler.Controllers.Api
{
    public class OffersController : ApiController
    {
        private HealthWholesalerDBEntities _context;

        public OffersController()
        {
            _context = new HealthWholesalerDBEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET /api/offers
        public IHttpActionResult GetOffers()
        {
            var offersdto = _context.Offers.ToList().Select(Mapper.Map<Offer,OffersDTO>);

            return Ok(offersdto);
        }

        // GET /api/offers/1
        public IHttpActionResult GetOffer(int id)
        {
            var offer = _context.Offers.SingleOrDefault(o => o.ID == id);
            if (offer == null)
                return NotFound(); 

            return Ok(Mapper.Map<Offer, OffersDTO>(offer));
        }

        // POST (Create) /api/offers

        [HttpPost]
        public IHttpActionResult CreateOffer(OffersDTO offerdto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            

            var offer = Mapper.Map<OffersDTO, Offer>(offerdto);

            _context.Offers.Add(offer);
            _context.SaveChanges();

            offerdto.ID = offer.ID;

            return Created(new Uri(Request.RequestUri + "/" + offer.ID), offerdto); 
        }

        // PUT (Update) /api/offers/1
        [HttpPut]
        public IHttpActionResult EditOffers(int id, OffersDTO offerdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var offerindb = _context.Offers.SingleOrDefault(o => o.ID == id);
            if (offerindb == null)
                return NotFound();

            Mapper.Map(offerdto, offerindb);

            //offerindb.ID = offerdto.ID;
            //offerindb.Name = offerdto.Name;
            //offerindb.Sale_Price = offerdto.Sale_Price;
            //offerindb.NumberInStock = offerdto.NumberInStock;
            //offerindb.CategoryId = offerdto.CategoryId;

            _context.SaveChanges();

            return Ok();
        }

        // DELETE offers
        [HttpDelete]
        public IHttpActionResult DeleteOffers(int id)
        {
            var offerindb = _context.Offers.SingleOrDefault(o => o.ID == id);
            if (offerindb == null)
                return NotFound();

            _context.Offers.Remove(offerindb);
            _context.SaveChanges();

            return Ok(); 
        }

    }
}
