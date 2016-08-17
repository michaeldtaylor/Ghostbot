using System.Collections.Generic;
using System.Text;

namespace Ghostbot.Modules.ClanWars.Model
{
    public static class ClanEventStatusRenderer
    {
        public static string RenderStatistics(ClanEventLeaderboardStatistics statistics)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Most Matches:       {statistics.MostMatches.Player.PadRight(20)} ({statistics.MostMatches.Result})");
            builder.AppendLine($"Most Points:        {statistics.MostPoints.Player.PadRight(20)} ({statistics.MostPoints.Result})");
            builder.AppendLine($"Hightest K/D:       {statistics.HighestKd.Player.PadRight(20)} ({statistics.HighestKd.Result})");
            builder.AppendLine($"Highest Win %:      {statistics.HighestWinPercentage.Player.PadRight(20)} ({statistics.HighestWinPercentage.Result})");
            builder.AppendLine($"Highest PPM:        {statistics.HighestPointsPerMatch.Player.PadRight(20)} ({statistics.HighestPointsPerMatch.Result})");
            builder.AppendLine($"Pew Pew:            {statistics.PewPew.Player.PadRight(20)} ({statistics.PewPew.Result})");
            builder.AppendLine($"Orbs:               {statistics.Orbs.Player.PadRight(20)} ({statistics.Orbs.Result})");
            builder.AppendLine();

            return builder.ToString();
        }

        public static string RenderClanMembers(int startingIndex, IEnumerable<ClanMemberRow> clanMemberRows)
        {
            var builder = new StringBuilder();

            startingIndex++;

            foreach (var row in clanMemberRows)
            {
                builder.AppendLine($"{startingIndex++.ToString("D2")}. {row.Name}");
                builder.AppendLine();
                builder.AppendLine($"Score:        {row.Score}");
                builder.AppendLine($"Matches:      {row.Matches}");
                builder.AppendLine($"PPM:          {row.PointsPerMatch}");
                builder.AppendLine($"Last Played:  {row.LastPlayedText}");
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}