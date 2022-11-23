using NewsApp.Models.GeoInfo;
using System.Net.Http;
using System.Text.Json;

namespace NewsApp.Services.GeoInfoProvider
{
    public class GeoInfoProviderService : IGeoInfoProviderService
    {
        private readonly HttpClient httpClient;
        public GeoInfoProviderService()
        {
            httpClient = new HttpClient();
        }

        public async Task<GeoInfoViewModel> GetGeoInfoDeserializedAsync()
        {
            string jsonResult = await GetGeoInfoAsync();
            var geoInfoObj = JsonSerializer.Deserialize<GeoInfoViewModel>(jsonResult, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return geoInfoObj;

        }

        private async Task<string> GetGeoInfoAsync()
        {
            var ipAddress = await GetIPAddressAsync();
            var response = await httpClient.GetAsync($"http://api.ipstack.com/" + ipAddress + "?access_key=6b183f58774551deac45d6e78f3badf7");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return json;
            }
            return null;
        }


        private async Task<string> GetIPAddressAsync()
        {
            var ipAddress = await httpClient.GetAsync($"http://ipinfo.io/ip");
            if (ipAddress.IsSuccessStatusCode)
            {
                var json = await ipAddress.Content.ReadAsStringAsync();
                return json.ToString();
            }
            return "";
        }
    }
}
