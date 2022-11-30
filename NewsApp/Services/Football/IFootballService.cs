using NewsApp.Models.Football;

namespace NewsApp.Services.Football
{
    public interface IFootballService
    {
        Task<StandingInfo> GetStandingObjectAsync(int season, int leagueId);
    }
}
