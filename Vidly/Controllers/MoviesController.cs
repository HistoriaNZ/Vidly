using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using System.Runtime.Caching;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        private static Random rand = new Random();

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movies/Random (will re-implement later).
        /*public ActionResult Random()
        {
            var viewModel = new RandomMovieViewModel
            {
                Movie = movie
            };
            return View(viewModel);
        }
        */

        //movies base
        public ViewResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            if (User.IsInRole(RoleName.CanManageMovies))
            {
                return View("List");
            }
            else
            {
                return View("ReadOnlyList");
            }
            
        }

        public ActionResult Details(int? id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(mov => mov.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(movie);
            }
        }

        public ActionResult Random()
        {
            var movies = _context.Movies.ToList();

            var randIndex = rand.Next(movies.Count);

            return RedirectToAction("Details", "Movies", new { movies[randIndex].Id });
        }

        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genre = _context.Genre.ToList();
            var viewModel = new MovieFormViewModel()
            {
                Genre = genre
            };
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genre = _context.Genre.ToList()
                };
                return View("MovieForm", viewModel);
            }
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                movie.NumberAvailable = Convert.ToByte(movie.NumberInStock);
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                //Calculate change in stock levels, if any
                int previousNumberInStock = movieInDb.NumberInStock;
                int newNumberInStock = movie.NumberInStock;
                byte moviesStockChanged = Convert.ToByte(newNumberInStock - previousNumberInStock);

                //asign new values to all appropriate fields
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.NumberAvailable += Convert.ToByte(moviesStockChanged);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genre = _context.Genre.ToList()
                };
                return View("MovieForm", viewModel);
            }
        }

    }
}