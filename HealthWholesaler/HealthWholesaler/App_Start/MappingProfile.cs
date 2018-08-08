using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using HealthWholesaler.Models;
using HealthWholesaler.DTOs; 

namespace HealthWholesaler.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping of Companies
            Mapper.CreateMap<Company, CompanyDTO>();
            Mapper.CreateMap<CompanyDTO, Company>();
            Mapper.CreateMap<DeliveryPackage, DeliveryPackageDTO>();

            // Mapping of Products
            Mapper.CreateMap<Product, ProductDTO>();
            Mapper.CreateMap<ProductDTO, Product>();

            // Mapping of Offers
            Mapper.CreateMap<Offer, OffersDTO>();
            Mapper.CreateMap<OffersDTO, Offer>();
        }
    }
}