using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAppAPI.Data;
using MovieAppAPI.Models;

namespace MovieAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly MovieContext _context;

        public ReviewsController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/Reviews/movie/{movieId}
        [HttpGet("movie/{movieId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsForMovie(int movieId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.MovieId == movieId)
                .ToListAsync();

            return Ok(reviews);
        }

        // PUT: api/Reviews/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, Review updatedReview)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            review.Comment = updatedReview.Comment;
            review.Rating = updatedReview.Rating;
            review.User = updatedReview.User;
            review.MovieId = updatedReview.MovieId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Reviews/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Reviews/movie/{movieId}/average
        [HttpGet("movie/{movieId}/average")]
        public async Task<ActionResult<double>> GetAverageRating(int movieId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.MovieId == movieId)
                .ToListAsync();

            if (reviews.Count == 0)
            {
                return Ok(0); // No reviews yet
            }

            var avgRating = reviews.Average(r => r.Rating);

            return Ok(avgRating);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Review>> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        [HttpPost]
        public ActionResult<Review> AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetReviews), new { id = review.Id }, review);
        }
    }
}
