using NewsApp.Models.GeoInfo;

namespace NewsApp.Services.GeoInfoProvider
{
    public interface IGeoInfoProviderService
    {
        Task<GeoInfoViewModel> GetGeoInfoDeserializedAsync();
    }
}
