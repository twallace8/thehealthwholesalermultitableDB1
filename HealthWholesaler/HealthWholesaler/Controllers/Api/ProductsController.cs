using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthWholesaler.Models;
using AutoMapper;
using HealthWholesaler.DTOs; 

namespace HealthWholesaler.Controllers.Api
{
    public class ProductsController : ApiController
    {
        private HealthWholesalerDBEntities _context;

        public ProductsController()
        {
            _context = new HealthWholesalerDBEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // 1) GET /api/products
        public IHttpActionResult GetCompanies()
        {
            var companydto = _context.Products.ToList().Select(Mapper.Map<Product, ProductDTO>);

            return Ok(); 
        }

        // 2) GET /api/products/1
        public IHttpActionResult GetProduct(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.ID == id);
            if (product == null)
                return NotFound(); 

            return Ok(Mapper.Map<Product, ProductDTO>(product));
        }

        // 3) POST (Create) /api/products
        [HttpPost]
        public IHttpActionResult CreateProduct(ProductDTO productdto)
        {
            if (!ModelState.IsValid)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest(); 
            }

            var product = Mapper.Map<ProductDTO, Product>(productdto);
            
            _context.Products.Add(product);
            _context.SaveChanges();

            productdto.ID = product.ID;

            return Created(new Uri(Request.RequestUri + "/" + product.ID), productdto);   
                
        }

        // 4) PUT (Update) /api/products/1
        [HttpPut]
        public IHttpActionResult UpdateProduct(ProductDTO productdto, int id)
        {
            if (!ModelState.IsValid)
            {
                // throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest(); 
            }

            var productindb = _context.Products.SingleOrDefault(p => p.ID == id);
            if (productindb == null)
                // throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound(); 

            //productindb.ID = productdto.ID;
            //productindb.Name = productdto.Name;
            //productindb.Price = productdto.Price;
            //productindb.NumberInStock = productdto.NumberInStock;
            //productindb.CategoryId = productdto.CategoryId;

            Mapper.Map(productdto, productindb);

            _context.SaveChanges();

            return Ok();

        }

        // 5) DELETE products
        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            var productindb = _context.Products.SingleOrDefault(p => p.ID == id);
            if (productindb == null)
                // throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound(); 

            _context.Products.Remove(productindb);
            _context.SaveChanges();

            return Ok(); 
        }

    }
}
