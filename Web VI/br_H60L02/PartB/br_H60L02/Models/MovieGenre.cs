namespace br_H60L02.Models
{
    public class MovieGenre
    {
        public long MovieGenreId { get; set; }
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
    }
}
