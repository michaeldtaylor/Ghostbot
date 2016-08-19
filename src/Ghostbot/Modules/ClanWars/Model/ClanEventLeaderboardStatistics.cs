using Ghostbot.Modules.ClanWars.Commands;

namespace Ghostbot.Modules.ClanWars.Model
{
    public class ClanEventLeaderboardStatistics
    {
        public ClanEventLeaderboardStatistics(LeaderboardElement mostMatches, LeaderboardElement mostPoints, LeaderboardElement highestKd, LeaderboardElement highestWinPercentage, LeaderboardElement highestPointsPerMatch, LeaderboardElement pewPew, LeaderboardElement orbs)
        {
            MostMatches = mostMatches;
            MostPoints = mostPoints;
            HighestKd = highestKd;
            HighestWinPercentage = highestWinPercentage;
            HighestPointsPerMatch = highestPointsPerMatch;
            PewPew = pewPew;
            Orbs = orbs;
        }

        public LeaderboardElement MostMatches { get; }
        public LeaderboardElement MostPoints { get; }
        public LeaderboardElement HighestKd { get; }
        public LeaderboardElement HighestWinPercentage { get; }
        public LeaderboardElement HighestPointsPerMatch { get; }
        public LeaderboardElement PewPew { get; }
        public LeaderboardElement Orbs { get; }
    }
}