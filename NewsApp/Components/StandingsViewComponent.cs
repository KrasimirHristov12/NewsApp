using Microsoft.AspNetCore.Mvc;
using NewsApp.Services.Football;

namespace NewsApp.Components
{
    public class StandingsViewComponent : ViewComponent
    {
        private readonly IFootballService footballService;

        public StandingsViewComponent(IFootballService footballService)
        {
            this.footballService = footballService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int season = 2022, int leagueId = 39)
        {
            var standingInfo = await footballService.GetStandingObjectAsync(season, leagueId);
            return View(standingInfo);
        }
    }
}
