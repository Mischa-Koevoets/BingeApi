using BingeClient.Models;
using Windows.UI.Xaml.Controls;
using System;
using BingeClient;
using Microsoft.UI.Xaml.Controls;

namespace BingeClient
{
    public sealed partial class DetailPage : Page
    {
        private readonly ApiService _apiService;
        private int _itemId;

        public DetailPage()
        {
            this.InitializeComponent();
            _apiService = new ApiService();
        }

        protected override async void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is int itemId)
            {
                _itemId = itemId;
                var item = await _apiService.GetMovieOrSeriesDetailAsync(itemId);

                if (item != null)
                {
                    TitleTextBox.Text = item.Title;
                    ReleaseYearTextBox.Text = item.ReleaseYear.ToString();
                    RatingTextBox.Text = item.Rating.ToString();
                }
            }
        }

        private async void SaveButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var updatedItem = new MovieOrSeries
            {
                Id = _itemId,
                Title = TitleTextBox.Text,
                ReleaseYear = int.Parse(ReleaseYearTextBox.Text),
                Rating = float.Parse(RatingTextBox.Text)
            };

            var success = await _apiService.UpdateMovieOrSeriesAsync(updatedItem);

            if (success)
            {
                // Optionally, navigate back or show a success message
                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                // Handle error
            }
        }
    }
}
