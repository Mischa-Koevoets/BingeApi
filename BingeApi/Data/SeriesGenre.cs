using System.Text.Json.Serialization;

namespace BingeApi.Data
{
    public class SeriesGenre
    {
        public int SeriesId { get; set; }

        [JsonIgnore]
        public Series Series { get; set; } = null!;
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
    }
}
