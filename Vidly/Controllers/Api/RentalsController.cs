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
using AutoMapper;

namespace Vidly.Controllers.Api
{
    
    public class RentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        //GET api/rentals
        public IHttpActionResult GetRentals()
        {
            var rentals = _context.Rentals.Include(r => r.Movie).
                Where(r => r.DateReturned == null).ToList();

            var mappedRentals = rentals.Select(Mapper.Map<Rental, ActiveRentalDto>);

            return Ok(mappedRentals);
        }

        //Takes customerId as parameter, NOT rental Id!
        public IHttpActionResult GetRental(int id)
        {
            var rentals = _context.Rentals.Where(r => r.CustomerId == id);

            if (rentals == null)
            {
                return NotFound();
            }
            var activeRentalDtos = rentals.Include(r => r.Movie).
                Where(r => r.DateReturned == null).ToList().
               Select(Mapper.Map<Rental, ActiveRentalDto>);

            return Ok(activeRentalDtos);

        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
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
                movie.NumberAvailable -= 1;

                _context.Rentals.Add(rental);
            }
            _context.SaveChanges();
            return Ok();
        }

        /* [HttpPut]
        public IHttpActionResult Return(NewRentalDto rentalDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var rentalsInDb = _context.Rentals.Where(r => r.CustomerId == rentalDto.custId);

            if (rentalsInDb == null)
            {
                return NotFound();
            }

            foreach (var rental in rentalsInDb)
            {
                rental.DateReturned = DateTime.Now;
            }

            _context.SaveChanges();

            return Ok();
        }
        */

        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult Return(ReturnsDto returnsDto)
        {
            //return Ok("movie list length: " + rentalDto.movieIds.Count());

            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var rentalsInDb = _context.Rentals.Include(r => r.Customer).
                Include(r => r.Movie).Where(r => returnsDto.RentalIds.Contains(r.Id)).
                ToList();

            //var rentalInDb = _context.Rentals.SingleOrDefault(r => r.Id == returnsDto.RentalIds[0]);

            foreach (var rental in rentalsInDb)
            {
                rental.DateReturned = DateTime.Now;
                rental.Movie.NumberAvailable += 1;
                _context.SaveChanges();
            }

            return Ok(rentalsInDb);
        }

    }
}