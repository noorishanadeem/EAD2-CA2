﻿using Microsoft.AspNetCore.Mvc;
using MovieAppAPI.Models;
using MovieAppAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        // GET: api/movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        // GET: api/movies/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();
            return movie;
        }

        // POST: api/movies
        [HttpPost]
        public async Task<ActionResult<Movie>> CreateMovie([FromBody] Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        // PUT: api/movies/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] Movie updatedMovie)
        {
            if (id != updatedMovie.Id)
                return BadRequest();

            _context.Entry(updatedMovie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Movies.Any(m => m.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/movies/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
