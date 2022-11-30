namespace NewsApp.Models.Football
{
    public class StandingInfo
    {
        public StandingInfo()
        {
            TeamsInfo = new List<TeamInfo>();
        }
        public string LeagueName { get; set; }
        public string LeagueCountry { get; set; }
        public string LeagueLogoUrl { get; set; }
        public int Season { get; set; }
        public ICollection<TeamInfo> TeamsInfo { get; set; }
    }
}
