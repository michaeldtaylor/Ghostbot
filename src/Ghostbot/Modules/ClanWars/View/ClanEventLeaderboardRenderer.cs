using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ghostbot.Modules.ClanWars.Model;

namespace Ghostbot.Modules.ClanWars.View
{
    public static class ClanEventLeaderboardRenderer
    {
        public static string RenderStatistics(ClanEventLeaderboard clanEventLeaderboard)
        {
            var statistics = clanEventLeaderboard.Statistics;
            var builder = new StringBuilder();

            builder.AppendLine(clanEventLeaderboard.Clan.Title);
            builder.AppendLine();
            builder.AppendLine($"Most Matches:       {statistics.MostMatches.Player.PadRight(20)} ({statistics.MostMatches.Result})");
            builder.AppendLine($"Most Points:        {statistics.MostPoints.Player.PadRight(20)} ({statistics.MostPoints.Result})");
            builder.AppendLine($"Hightest K/D:       {statistics.HighestKd.Player.PadRight(20)} ({statistics.HighestKd.Result})");
            builder.AppendLine($"Highest Win %:      {statistics.HighestWinPercentage.Player.PadRight(20)} ({statistics.HighestWinPercentage.Result})");
            builder.AppendLine($"Highest PPM:        {statistics.HighestPointsPerMatch.Player.PadRight(20)} ({statistics.HighestPointsPerMatch.Result})");

            if (statistics.PewPew != null)
            {
                builder.AppendLine($"Pew Pew:            {statistics.PewPew.Player.PadRight(20)} ({statistics.PewPew.Result})");
            }

            if (statistics.Orbs != null)
            {
                builder.AppendLine($"Orbs:               {statistics.Orbs.Player.PadRight(20)} ({statistics.Orbs.Result})");
            }

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

        public static string RenderEvent(ClanEventLeaderboard clanEventLeaderboard)
        {
            var @event = clanEventLeaderboard.Event;
            var longestModifierName = clanEventLeaderboard.Event.Modifiers.Max(c => c.Name.Length);
            
            var builder = new StringBuilder();

            builder.AppendLine($"Event:        {@event.Title} ({@event.Id})");
            builder.AppendLine($"Description:  {@event.Description}");
            builder.AppendLine();

            var index = 1;

            foreach (var modifier in clanEventLeaderboard.Event.Modifiers)
            {
                var modifierName = $"{modifier.Name}:".PadRight(longestModifierName + 4);

                builder.AppendLine($"{index++.ToString("D2")}. {modifierName}{modifier.Value}");
                builder.AppendLine();
                builder.AppendLine($"{modifier.Description}");
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}