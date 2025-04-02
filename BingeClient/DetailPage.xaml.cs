using BingeClient.Models;
using Windows.UI.Xaml.Controls;
using System;
using BingeClient;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml;

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

        protected override async void OnNavigatedTo(NavigationEventArgs e)
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

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var updatedItem = new MovieOrSeries
            {
                Id = _itemId,
                Title = TitleTextBox.Text,
                ReleaseYear = int.Parse(ReleaseYearTextBox.Text),
                Rating = float.Parse(RatingTextBox.Text)
            };

            var success = await _apiService.UpdateMovieOrSeriesAsync(updatedItem);

            Frame.Navigate(typeof(MainPage));

        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var itemToDelete = new MovieOrSeries { Id = _itemId };
            var success = await _apiService.DeleteMovieOrSeriesAsync(itemToDelete);

            if (success)
            {
                Frame.Navigate(typeof(MainPage));
            }
        }
    }
}
