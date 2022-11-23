using NewsApp.Models.Weather;

namespace NewsApp.Services.Weather
{
    public interface IWeatherService
    {
        Task<WeatherViewModel> GetWeatherAsync(decimal longitude, decimal latitude);
    }
}
