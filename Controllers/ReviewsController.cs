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

        // Get all reviews for a specific movie
        [HttpGet("movie/{movieId}")]
        public ActionResult<IEnumerable<Review>> GetReviewsForMovie(int movieId)
        {
            var reviews = _context.Reviews.Where(r => r.MovieId == movieId).ToList();
            return reviews;
        }

        // Add a new review
        [HttpPost]
        public ActionResult<Review> AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetReviewsForMovie), new { movieId = review.MovieId }, review);
        }

        // Update (edit) a review
        [HttpPut("{id}")]
        public IActionResult UpdateReview(int id, Review updatedReview)
        {
            var review = _context.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }

            review.User = updatedReview.User;
            review.Comment = updatedReview.Comment;
            review.Rating = updatedReview.Rating;
            review.MovieId = updatedReview.MovieId;

            _context.SaveChanges();

            return NoContent();
        }

        // Delete a review
        [HttpDelete("{id}")]
        public IActionResult DeleteReview(int id)
        {
            var review = _context.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
