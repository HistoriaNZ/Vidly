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
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetRentals()
        {
            var rentals = _context.Rentals.
                Where(r => r.DateReturned == null).ToList();

            return Ok(rentals);
        }

        [HttpPost]
        public IHttpActionResult Create(NewRentalDto newRental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something's wrong with the submission.");
            }

            if (newRental.movieIds.Count == 0)
            {
                return BadRequest("You haven't selected any movies.");
            }

            var customer = _context.Customers.SingleOrDefault(c => c.Id == newRental.custId);

            if (customer == null)
            {
                return BadRequest("Invalid customer ID.");
            }



            var movies = _context.Movies.Where(m => newRental.movieIds.Contains(m.Id)).ToList();

            if (movies.Count != newRental.movieIds.Count)
            {
                return BadRequest("One or more movies are invalid.");
            }

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                {
                    return BadRequest("This movie is not available.");
                }

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