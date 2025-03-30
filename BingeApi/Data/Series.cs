namespace BingeApi.Data
{
    public class Series
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public double Rating { get; set; }
        public List<SeriesGenre> SeriesGenres { get; set; } = new();
    }
}
