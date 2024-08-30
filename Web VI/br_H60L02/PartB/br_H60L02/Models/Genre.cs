namespace br_H60L02.Models
{
    public class Genre
    {
        public long GenreId { get; set; }
        public string GenreName { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
