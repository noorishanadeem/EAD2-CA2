using System.ComponentModel.DataAnnotations;

namespace MovieAppAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Genre { get; set; } = string.Empty;

        [Required]
        public string Director { get; set; } = string.Empty;

        [Required]
        public string Producer { get; set; } = string.Empty;

        [Range(0, 5)]
        public double Rating { get; set; }

        [Required]
        public string PgRating { get; set; } = string.Empty;

        
        public int Length { get; set; } 

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string PosterUrl { get; set; } = string.Empty;

    }
}
