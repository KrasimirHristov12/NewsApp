using Microsoft.AspNetCore.WebUtilities;
using NewsApp.Models.Football;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;

namespace NewsApp.Services.Football
{
    public class FootballService : IFootballService
    {
        private async Task<string> GetStandingsJsonAsync(int season, int leagueId)
        {
            var client = new HttpClient();

            string url = $"https://api-football-beta.p.rapidapi.com/standings";
            var parameters = new Dictionary<string, string>();
            parameters.Add("season", season.ToString());
            parameters.Add("league", leagueId.ToString());
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(QueryHelpers.AddQueryString(url, parameters)),
                Method = HttpMethod.Get,
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("X-RapidAPI-Key", "fad38f7576msh239314bc28a19f2p114b83jsndf2235278d1e");
            request.Headers.Add("X-RapidAPI-Host", "api-football-beta.p.rapidapi.com");




            var response = await client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<StandingInfo> GetStandingObjectAsync(int season, int leagueId)
        {

            string json = await GetStandingsJsonAsync(season, leagueId);

            var jsonDocument = JsonDocument.Parse(json);
            var response = jsonDocument.RootElement.GetProperty("response");
            var responseInner = response.EnumerateArray().First();

            var teamsInfo = responseInner.GetProperty("league").GetProperty("standings").EnumerateArray().First().EnumerateArray();

            var standingInfo = new StandingInfo();
            string leagueName = responseInner.GetProperty("league").GetProperty("name").GetString();
            string leagueCountry = responseInner.GetProperty("league").GetProperty("country").GetString();
            string leagueLogoUrl = responseInner.GetProperty("league").GetProperty("logo").GetString();
            standingInfo.LeagueName = leagueName;
            standingInfo.LeagueCountry = leagueCountry;
            standingInfo.LeagueLogoUrl = leagueLogoUrl;
            standingInfo.Season = season;


            foreach (var teamObj in teamsInfo)
            {

                int rank = teamObj.GetProperty("rank").GetInt32();
                string teamName = teamObj.GetProperty("team").GetProperty("name").GetString();
                string teamLogo = teamObj.GetProperty("team").GetProperty("logo").GetString();
                int points = teamObj.GetProperty("points").GetInt32();
                int goalsDiff = teamObj.GetProperty("goalsDiff").GetInt32();
                string form = teamObj.GetProperty("form").GetString();
                int played = teamObj.GetProperty("all").GetProperty("played").GetInt32();
                int win = teamObj.GetProperty("all").GetProperty("win").GetInt32();
                int draw = teamObj.GetProperty("all").GetProperty("draw").GetInt32();
                int lose = teamObj.GetProperty("all").GetProperty("lose").GetInt32();
                int goalsFor = teamObj.GetProperty("all").GetProperty("goals").GetProperty("for").GetInt32();
                int goalsAgainst = teamObj.GetProperty("all").GetProperty("goals").GetProperty("against").GetInt32();

                var teamInfo = new TeamInfo
                {
                    Rank = rank,
                    TeamName = teamName,
                    TeamLogo = teamLogo,
                    Points = points,
                    GoalsDiff = goalsDiff,
                    Form = form,
                    Played = played,
                    Win = win,
                    Draw = draw,
                    Lose = lose,
                    GoalsFor = goalsFor,
                    GoalsAgainst = goalsAgainst,
                };
                standingInfo.TeamsInfo.Add(teamInfo);
            }



            return standingInfo;



        }
    }
}
