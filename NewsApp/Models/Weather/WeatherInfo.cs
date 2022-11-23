using System.Text.Json.Serialization;

namespace NewsApp.Models.Weather
{
    public class WeatherInfo
    {
        [JsonPropertyName("app_temp")]
        public decimal AppTemp { get; set; }
        [JsonPropertyName("city_name")]
        public string CityName { get; set; }
    }
}
