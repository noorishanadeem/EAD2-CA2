using Microsoft.AspNetCore.Mvc;
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
