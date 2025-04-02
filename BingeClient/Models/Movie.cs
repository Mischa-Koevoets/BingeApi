using System.Collections.Generic;

namespace BingeClient.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public double Rating { get; set; }
        public List<MovieGenre> MovieGenres { get; set; } = new();
    }
}
