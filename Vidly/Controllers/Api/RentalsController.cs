using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class RentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
        }

        [HttpPost]
        public IHttpActionResult Create(NewRentalDto newRental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = _context.Customers.SingleOrDefault(c => c.Id == newRental.custId);

            if (customer == null)
            {
                return BadRequest("Invalid customer ID.");
            }

            var movies = _context.Movies.Where(m => newRental.movieIds.Contains(m.Id));

            foreach (var movie in movies)
            {
                var rental = new Rental();
                rental.DateRented = DateTime.Now;
                rental.Customer = customer;
                rental.Movie = movie;

                _context.Rentals.Add(rental);
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}