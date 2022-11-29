using Microsoft.AspNetCore.WebUtilities;
using NewsApp.Models.Weather.Weather;
using NewsApp.Services.GeoInfoProvider;
using System.Net.Http.Headers;
using System.Text.Json;

namespace NewsApp.Services.Weather
{
    public class WeatherService : IWeatherService
    {
        public async Task<WeatherViewModel> GetWeatherAsync(double longitude, double latitude)
        {
            var client = new HttpClient();

            string url = "https://weatherbit-v1-mashape.p.rapidapi.com/current";
            var parameters = new Dictionary<string, string>();
            parameters.Add("lat", latitude.ToString());
            parameters.Add("lon", longitude.ToString());
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(QueryHelpers.AddQueryString(url, parameters)),
                Method = HttpMethod.Get,
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("X-RapidAPI-Key", "fad38f7576msh239314bc28a19f2p114b83jsndf2235278d1e");
            request.Headers.Add("X-RapidAPI-Host", "weatherbit-v1-mashape.p.rapidapi.com");




            var response = await client.SendAsync(request);

            var contentAsJson = await response.Content.ReadAsStringAsync();

            WeatherViewModel weather = JsonSerializer.Deserialize<WeatherViewModel>(contentAsJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,

            });
            return weather;
        }
    }
}
