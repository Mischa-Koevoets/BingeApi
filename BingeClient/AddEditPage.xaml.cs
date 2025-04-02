using BingeClient;
using BingeClient.Models;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BingeTrackerApp.Pages
{
    public sealed partial class AddEditPage : Page
    {
        private readonly ApiService _apiService;
        private int? _itemId;

        public AddEditPage()
        {
            this.InitializeComponent();
            _apiService = new ApiService();
            FillGenres();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is MovieOrSeries item)
            {
                _itemId = item.Id;
                TitleTextBox.Text = item.Title;
                ReleaseYearTextBox.Text = item.ReleaseYear.ToString();
                RatingTextBox.Text = item.Rating.ToString();
                TypeComboBox.SelectedIndex = item.IsMovie ? 0 : 1;
            }
        }

        private async void FillGenres()
        {
            var genres = await _apiService.GetGenresAsync();
            foreach (var genre in genres)
            {
                GenreComboBox.Items.Add(genre);
            }
        }



        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var newItem = new MovieOrSeries
            {
                Id = _itemId ?? 0,
                Title = TitleTextBox.Text,
                ReleaseYear = int.Parse(ReleaseYearTextBox.Text),
                Rating = float.Parse(RatingTextBox.Text),
                IsMovie = TypeComboBox.SelectedIndex == 0
            };

            bool success;
            if (_itemId.HasValue)
                success = await _apiService.UpdateMovieOrSeriesAsync(newItem);
            else
                success = await _apiService.CreateMovieOrSeriesAsync(newItem);

            if (success)
                Frame.Navigate(typeof(MainPage));
        }
    }
}
