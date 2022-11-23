using Microsoft.AspNetCore.Mvc;
using NewsApp.Services.GeoInfoProvider;
using NewsApp.Services.Weather;

namespace NewsApp.Components
{
    public class WeatherViewComponent : ViewComponent
    {
        private readonly IWeatherService weatherService;
        private readonly IGeoInfoProviderService geoInfo;

        public WeatherViewComponent(IWeatherService weatherService, IGeoInfoProviderService geoInfo)
        {
            this.weatherService = weatherService;
            this.geoInfo = geoInfo;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var geoInfoObj = await geoInfo.GetGeoInfoDeserializedAsync();
            var weather = await weatherService.GetWeatherAsync(geoInfoObj.Longitude, geoInfoObj.Latitude);
            return View(weather);

        }
    }
}
