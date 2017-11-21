using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class RentalsController
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
        }

        [HttpPost]
        public IHttpActionResult Create(NewRentalDto newRental)
        {
            throw new NotImplementedException();
        }
    }
}