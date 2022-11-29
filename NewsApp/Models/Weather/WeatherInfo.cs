using System.Text.Json.Serialization;

namespace NewsApp.Models.Weather.Weather
{
    public class WeatherInfo
    {
        [JsonPropertyName("app_temp")]
        public double FeelTemp { get; set; }

        public double Temp { get; set; }
        [JsonPropertyName("city_name")]
        public string CityName { get; set; }

        [JsonPropertyName("rh")]
        public int RelativeHumidity { get; set; }


        public WeatherCloudInfo Weather { get; set; }

    }
}
