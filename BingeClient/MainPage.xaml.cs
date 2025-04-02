using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BingeClient.Models;
using BingeClient;
using BingeTrackerApp.Pages;

namespace BingeClient
{
    public sealed partial class MainPage : Page
    {
        private readonly ApiService _apiService = new ApiService();
        public ObservableCollection<MovieOrSeries> MoviesSeries { get; set; } = new();

        public MainPage()
        {
            this.InitializeComponent();
            LoadMoviesAndSeries();

        }

        private async void LoadMoviesAndSeries()
        {
            var items = await _apiService.GetMoviesAndSeriesAsync();
            MoviesSeries.Clear();
            foreach (var item in items)
            {
                MoviesSeries.Add(item);
            }
            MoviesSeriesList.ItemsSource = MoviesSeries;
        }

        

        private void AddMovieSeries_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddEditPage));
        }

        private void MoviesSeriesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MoviesSeriesList.SelectedItem is MovieOrSeries selected)
            {
                Frame.Navigate(typeof(DetailPage), selected);
            }
        }
    }
}