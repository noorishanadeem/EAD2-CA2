using System.ComponentModel.DataAnnotations;

namespace MovieAppAPI.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string User { get; set; }
        public string Comment { get; set; }

        [Range(0, 5)]
        public double Rating { get; set; }

    }
}
