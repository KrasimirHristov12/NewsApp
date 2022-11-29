using NewsApp.Models.Weather.Weather;

namespace NewsApp.Services.Weather
{
    public interface IWeatherService
    {
        Task<WeatherViewModel> GetWeatherAsync(double longitude, double latitude);
    }
}
