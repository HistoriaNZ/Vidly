using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;



namespace Vidly.Controllers
{
    public class RentalsController : Controller
    {
        // GET: Rentals
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            return View();
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Return()
        {
            return View();
        }
    }
}