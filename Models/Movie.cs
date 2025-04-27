namespace MovieAppAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Producer { get; set; }
        public double Rating { get; set; }
        public string PgRating { get; set; }
        public int Length { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
