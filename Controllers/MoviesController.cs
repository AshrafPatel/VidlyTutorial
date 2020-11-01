using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using AutoMapper;

namespace Vidly.Controllers
{
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


        //GET: Movies
        public ViewResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");
            return View("ReadOnlyList");
        }


        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Jumanji" };
            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1" },
                new Customer {Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Customers = customers,
                Movie = movie
            };
            return View(viewModel);
        }


        // GET: Movies/released/year/month
        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(month + "/" + year) ;
        }


        //GET: Movies/Details/Id
        [Route("movies/details/{id}")]
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genreList = _context.Genres.ToList();
            var movie = new Movie
            {
                NumberInStock = 0
            };

            var formMovieViewModel = new FormMovieViewModel
            {
                Genres = genreList,
                Movie = movie
            };

            return View("MovieForm", formMovieViewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var genreList = _context.Genres.ToList();
            var formMovieViewModel = new FormMovieViewModel
            {
                Genres = genreList,
                Movie = movie
            };

            return View("MovieForm", formMovieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var genreList = _context.Genres.ToList();
                var formMovieViewModel = new FormMovieViewModel
                {
                    Movie = movie,
                    Genres = genreList
                };

                return View("MovieForm", formMovieViewModel);
            }
            if (movie.Id == 0)
                _context.Movies.Add(movie);
            else
            {
                var moviesInDb = _context.Movies.SingleOrDefault(m => m.Id == movie.Id);
                Mapper.Map<Movie, Movie>(moviesInDb, movie);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }
    }
}