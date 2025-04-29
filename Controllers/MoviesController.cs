using Microsoft.AspNetCore.Mvc;
using MovieAppAPI.Models;
using System.Collections.Generic;

namespace MovieAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        // This will act like a fake database for now
        private static List<Movie> movies = new List<Movie>();

        // GET: api/movies
        [HttpGet]
        public ActionResult<IEnumerable<Movie>> GetMovies()
        {
            return Ok(movies);
        }

        // GET: api/movies/{id}
        [HttpGet("{id}")]
        public ActionResult<Movie> GetMovie(int id)
        {
            var movie = movies.Find(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        // POST: api/movies
        [HttpPost]
        public ActionResult<Movie> CreateMovie([FromBody] Movie movie)
        {
            movie.Id = movies.Count + 1; // Simple ID assignment
            movies.Add(movie);
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        // PUT: api/movies/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateMovie(int id, [FromBody] Movie updatedMovie)
        {
            var movie = movies.Find(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            // Update properties
            movie.Title = updatedMovie.Title;
            movie.Description = updatedMovie.Description;
            movie.Genre = updatedMovie.Genre;
            movie.Director = updatedMovie.Director;
            movie.Producer = updatedMovie.Producer;
            movie.Rating = updatedMovie.Rating;
            movie.PgRating = updatedMovie.PgRating;
            movie.Length = updatedMovie.Length;
            movie.ReleaseDate = updatedMovie.ReleaseDate;

            return NoContent();
        }

        // DELETE: api/movies/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteMovie(int id)
        {
            var movie = movies.Find(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            movies.Remove(movie);
            return NoContent();
        }
    }
}
