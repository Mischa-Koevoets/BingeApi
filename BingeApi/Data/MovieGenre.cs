using System.Text.Json.Serialization;

namespace BingeApi.Data
{
    public class MovieGenre
    {
        public int MovieId { get; set; }

        [JsonIgnore]
        public Movie Movie { get; set; } = null!;
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
    }
}
