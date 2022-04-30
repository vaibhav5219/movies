using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Movies.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var user = HttpContext.User as ClaimsPrincipal;

            if (!user.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                ViewBag.Message = "Your application description page.";
            }
            return View();

            var minAge = 16;
            var dateOfBirth = Convert.ToDateTime(user.FindFirst(c =>
               c.Type == ClaimTypes.DateOfBirth).Value);

            if (calculateAge(dateOfBirth) >= minAge)
            {
                ViewBag.Message = "You can view this page.";
            }
            else
            {
                ViewBag.Message = "Your cannot view this page - your age is bellow permitted one.";
            }

            return View();
        }

        private int calculateAge(DateTime dateOfBirth)
        {
            int calculatedAge = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
            {
                calculatedAge--;
            }
            return calculatedAge;
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}