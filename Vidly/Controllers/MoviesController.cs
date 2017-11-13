using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
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

        public ActionResult Edit(int id)
        {
            return Content("id=" + id);
        }

        //movies base
        public ViewResult Index()
        {
            var movies = GetMovies();

            return View(movies);
        }

        public ActionResult Details(int? id)
        {
            var movie = GetMovies().SingleOrDefault(mov => mov.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(movie);
            }
        }

        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie { Id = 1, Name = "Shrek" },
                new Movie { Id = 2, Name = "Wall-E"}
            };
        }

    }
}