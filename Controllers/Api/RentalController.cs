using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class RentalController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateRental(RentalDto rentalDto)
        {
            //Edge Cases
            // Customer ID Invalid
            // Movie IDs Invalid => Wrong or not typed
            // Movies are not available

            if (rentalDto.MovieIds.Count == 0)
                return BadRequest("No Movie IDs were provided");

            var rentalCustomer = _context.Customers.SingleOrDefault(c => c.Id == rentalDto.CustomerId);

            if (rentalCustomer == null)
                return BadRequest("Customer Id is not valid");

            var movies = _context.Movies.Where(m => rentalDto.MovieIds.Contains(m.Id)).ToList();

            if (rentalDto.MovieIds.Count != movies.Count)
                return BadRequest("One or more movies could not be found");

            foreach(Movie movie in movies) {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = rentalCustomer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();
            return Ok();
        }
    }
}
