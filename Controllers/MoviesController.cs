using Microsoft.AspNetCore.Mvc;
using MovieAppAPI.Data;
using MovieAppAPI.Models;

namespace MovieAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> GetMovies()
        {
            return _context.Movies.ToList();
        }

        [HttpPost]
        public ActionResult<Movie> AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetMovies), new { id = movie.Id }, movie);
        }
    }
}
