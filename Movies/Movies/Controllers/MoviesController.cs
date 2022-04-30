using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movies.Controllers
{

    [Authorize]
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movie
        [AllowAnonymous]
        public ActionResult Index()
        {

            var movies = _context.Movies.ToList();
            if (User.IsInRole("Guest"))
            {
                return View("ViewList",movies);
            }
            return View(movies);
        }
        [Authorize(Roles = "Admin,CanEditMovies")]
        public ViewResult New()
        {
            var movie = new Movie();
            movie.ReleaseDate = DateTime.Now;
            return View("MovieForm", movie);
        }
        [Authorize(Roles =RoleName.Guest)]
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);

        }
        [Authorize(Roles = RoleName.CanEditMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View("MovieForm",movie);
        }
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var movies = new Movie();
                return View("MovieForm", movies);
            }
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;

                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

    }
}