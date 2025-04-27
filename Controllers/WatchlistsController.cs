using Microsoft.AspNetCore.Mvc;
using MovieAppAPI.Data;
using MovieAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WatchlistsController : ControllerBase
    {
        private readonly MovieContext _context;

        public WatchlistsController(MovieContext context)
        {
            _context = context;
        }

        [HttpGet("{user}")]
        public ActionResult<IEnumerable<Movie>> GetWatchlist(string user)
        {
            // Get all movieIds for the user
            var movieIds = _context.Watchlists
                .Where(w => w.User == user)
                .Select(w => w.MovieId)
                .ToList();

            // Get full movie details for those movieIds
            var movies = _context.Movies
                .Where(m => movieIds.Contains(m.Id))
                .ToList();

            return movies;
        }

        [HttpPost]
        public ActionResult<Watchlist> AddToWatchlist(Watchlist watchlist)
        {
            _context.Watchlists.Add(watchlist);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetWatchlist), new { user = watchlist.User }, watchlist);
        }
        [HttpDelete("{user}/{movieId}")]
        public async Task<IActionResult> RemoveFromWatchlist(string user, int movieId)
        {
            var item = await _context.Watchlists
                .FirstOrDefaultAsync(w => w.User == user && w.MovieId == movieId);
            if (item == null)
                return NotFound();

            _context.Watchlists.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
