using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization; // Add this
using System.Threading.Tasks;
using BingeClient.Models;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "http://localhost:5000/api"; // Pas dit aan indien nodig

    public ApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<MovieOrSeries[]> GetMoviesAndSeriesAsync()
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/movie");
        var movies = Array.Empty<MovieOrSeries>();
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Movies JSON: " + json); // Log the JSON response
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve // Add this line
            };
            movies = JsonSerializer.Deserialize<MovieOrSeries[]>(json, options);
        }

        response = await _httpClient.GetAsync($"{BaseUrl}/series");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Series JSON: " + json); // Log the JSON response
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve // Add this line
            };
            var series = JsonSerializer.Deserialize<MovieOrSeries[]>(json, options);
            movies = movies.Concat(series).ToArray();
        }
        return movies;
    }

    public async Task<Genre[]> GetGenresAsync()
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/genre");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve // Add this line
            };
            return JsonSerializer.Deserialize<Genre[]>(json, options);
        }
        return Array.Empty<Genre>();
    }

    public async Task<MovieOrSeries> GetMovieOrSeriesDetailAsync(int id)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/movie/{id}");

        if (!response.IsSuccessStatusCode)
        {
            response = await _httpClient.GetAsync($"{BaseUrl}/series/{id}");
        }

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve // Add this line
            };
            return JsonSerializer.Deserialize<MovieOrSeries>(json, options);
        }

        return null;
    }

    public async Task<bool> UpdateMovieOrSeriesAsync(MovieOrSeries item)
    {
        string endpoint = item.IsMovie ? "Movie" : "Series";
        var json = JsonSerializer.Serialize(item);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"{BaseUrl}/{endpoint}/{item.Id}", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateMovieOrSeriesAsync(MovieOrSeries item)
    {
        string endpoint = item.IsMovie ? "Movie" : "Series";
        var json = JsonSerializer.Serialize(item);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{BaseUrl}/{endpoint}", content);
        return response.IsSuccessStatusCode;
    }

   
    public async Task<bool> DeleteMovieOrSeriesAsync(MovieOrSeries item)
    {
        string endpoint = item.IsMovie ? "Movie" : "Series";
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/{endpoint}/{item.Id}");
        return response.IsSuccessStatusCode;
    }
}
