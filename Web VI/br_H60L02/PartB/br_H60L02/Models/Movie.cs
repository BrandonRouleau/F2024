
namespace br_H60L02.Models;
    using System.Collections.Generic;

    public class Movie
    {
        public long MovieId { get; set; }
        public string Title { get; set; }
        public decimal Rating { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }

