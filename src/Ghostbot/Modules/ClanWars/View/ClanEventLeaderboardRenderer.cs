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

            foreach (var statistic in statistics)
            {
                var paddedName = $"{statistic.Name}:";

                builder.AppendLine($"{paddedName.PadRight(20)}{statistic.Player.PadRight(20)}({statistic.Result})");
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