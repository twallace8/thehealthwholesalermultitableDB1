using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthWholesaler.Models;
using System.Web.Mvc;
using HealthWholesaler.DTOs;
using System.Data.Entity;
using AutoMapper;

namespace HealthWholesaler.Controllers.Api
{
    public class CompaniesController : ApiController
    {
        private HealthWholesalerDBEntities _context;

        public CompaniesController()
        {
            _context = new HealthWholesalerDBEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET /api/companies  
        public IHttpActionResult GetCompanies()
        {
             var customerDtos = _context.Companies
                .Include(c => c.DeliveryPackage)
                .ToList()
                // mapping company to company DTO 
                .Select(Mapper.Map<Company, CompanyDTO>);

            return Ok(customerDtos); 
        }

        // GET /api/companies/1
        public IHttpActionResult Company(int id)
        {
            var company = _context.Companies.SingleOrDefault(c => c.ID == id);
            if (company == null)
                return NotFound(); 

            return Ok(Mapper.Map<Company, CompanyDTO>(company));
        }

        // POST (Create) /api/companies

        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateCompany (CompanyDTO companydto)
        {
            if (!ModelState.IsValid)
                return BadRequest(); 

            var company = Mapper.Map<CompanyDTO, Company>(companydto);
            _context.Companies.Add(company);
            _context.SaveChanges();

            companydto.ID = company.ID;

            return Created(new Uri(Request.RequestUri + "/" + company.ID), companydto);
        }

        [System.Web.Http.HttpPut]

        // PUT (Edit) /api/companies/1
        public IHttpActionResult UpdateCompany(int id, CompanyDTO companydto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(); 
            }

            var companyindb = _context.Companies.SingleOrDefault(c => c.ID == id);
            if (companyindb == null)
                return NotFound(); 

            Mapper.Map(companydto, companyindb); 
            //companyindb.ID = companydto.ID;
            //companyindb.Name = companydto.Name;
            //companyindb.Headquarters = companydto.Headquarters;
            //companyindb.DeliveryPackageId = companydto.DeliveryPackageId;

            _context.SaveChanges();

            return Ok(); 
        }

        // DELETE Company

        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteCompany(int id)
        {
            var companyindb = _context.Companies.SingleOrDefault(c => c.ID == id);
            if (companyindb == null)
                return NotFound();

            _context.Companies.Remove(companyindb);
            _context.SaveChanges();

            return Ok(); 
        }
    }
}
