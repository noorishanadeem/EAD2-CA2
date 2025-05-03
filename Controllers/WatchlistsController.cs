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
        public async Task<ActionResult<IEnumerable<Movie>>> GetWatchlist(string user)
        {
            if (string.IsNullOrWhiteSpace(user))
                return BadRequest("User cannot be null or empty.");

            try
            {
                var movieIds = await _context.Watchlists
                    .Where(w => w.User == user)
                    .Select(w => w.MovieId)
                    .ToListAsync();

                if (movieIds == null || !movieIds.Any())
                    return NotFound("No watchlist found for user '{user}'.");

                var movies = await _context.Movies
                    .Where(m => movieIds.Contains(m.Id))
                    .ToListAsync();

                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: {ex.Message}");
            }
        }


        // adding to watchlist
        [HttpPost]
        public ActionResult<Watchlist> AddToWatchlist(Watchlist watchlist)
        {
            _context.Watchlists.Add(watchlist);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetWatchlist), new { user = watchlist.User }, watchlist);
        }

        // removing from watchlist
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
