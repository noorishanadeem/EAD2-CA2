using Microsoft.EntityFrameworkCore;
using MovieAppAPI.Models;
using System.Collections.Generic;

namespace MovieAppAPI.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }
    }
}
