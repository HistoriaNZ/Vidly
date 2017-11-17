using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Models;
using Vidly.Dtos;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDto>();
            // Don't map DTO Id to _context Id when updating a customer.
            Mapper.CreateMap<CustomerDto, Customer>().
                ForMember(c => c.Id, opt => opt.Ignore()); ;


            Mapper.CreateMap<Movie, MovieDto>();
            // Don't map DTO Id to _context Id when updating a movie.
            Mapper.CreateMap<MovieDto, Movie>().
                ForMember(m => m.Id, opt => opt.Ignore());    
        }
    } 
}