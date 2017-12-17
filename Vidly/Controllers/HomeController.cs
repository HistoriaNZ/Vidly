using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Vidly.Controllers
{
    public class HomeController : Controller
    {

        /* Syntax for disabled caching on an action:
         * [OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)]
         */
        [AllowAnonymous]
        [OutputCache(Duration = 50, Location = OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            throw new Exception();
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}