using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NewsApp.Data;
using NewsApp.Models.Weather.Weather;
using NewsApp.Services.GeoInfoProvider;
using NewsApp.Services.Weather;
using Newtonsoft.Json;

namespace NewsApp.Components
{
    public class WeatherViewComponent : ViewComponent
    {
        private readonly IWeatherService weatherService;
        private readonly IGeoInfoProviderService geoInfo;
        private readonly IDistributedCache distributedCache;

        public WeatherViewComponent(IWeatherService weatherService, IGeoInfoProviderService geoInfo, IDistributedCache distributedCache)
        {
            this.weatherService = weatherService;
            this.geoInfo = geoInfo;
            this.distributedCache = distributedCache;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {


            var geoInfoObj = await geoInfo.GetGeoInfoDeserializedAsync();
            if (geoInfoObj == null)
            {
                return View(geoInfoObj);
            }
            

            var weatherInfo = await this.distributedCache.GetStringAsync("WeatherInfo");
            WeatherViewModel weather = null;

            if (weatherInfo == null)
            {
                weather = await weatherService.GetWeatherAsync(geoInfoObj.Longitude, geoInfoObj.Latitude);
                await this.distributedCache.SetStringAsync("WeatherInfo", JsonConvert.SerializeObject(weather),
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    });
            }
            else
            {
                weather = JsonConvert.DeserializeObject<WeatherViewModel>(weatherInfo);
            }
            return View(weather);
           

        }
    }
}
