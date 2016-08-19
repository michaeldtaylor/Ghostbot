using System.Text;
using Ghostbot.Modules.ClanWars.Model;

namespace Ghostbot.Modules.ClanWars.View
{
    public class ChallengeStatusWideRenderer : IChallengeStatusRenderer
    {
        public ChallengeStatusFormat Format => ChallengeStatusFormat.Wide;

        public string RenderHeader(ChallengeStatus challengeStatus)
        {
            var header = challengeStatus.Details;
            var @event = challengeStatus.Event;
            var builder = new StringBuilder();

            builder.AppendLine($"Issuer: {header.Issuer}");
            builder.AppendLine($"Event:  {@event.Title} ({@event.Uri})");
            builder.AppendLine($"Dates:  {header.FromDate} - {header.ToDate}");
            builder.AppendLine();
            builder.AppendLine($"{nameof(ClanChallengeStatusRow.Rank).PadRight(6)}\t{nameof(ClanChallengeStatusRow.Clan).PadRight(30)}\t{nameof(ClanChallengeStatusRow.Score).PadRight(10)}\t{nameof(ClanChallengeStatusRow.Active).PadRight(10)}\t{nameof(ClanChallengeStatusRow.Total).PadRight(10)}");

            return builder.ToString();
        }

        public string RenderClans(ChallengeStatus challengeStatus)
        {
            var builder = new StringBuilder();

            foreach (var row in challengeStatus.Rows)
            {
                builder.AppendLine(RenderClan(row, challengeStatus.Event));
            }

            return builder.ToString();
        }

        public string RenderClan(ClanChallengeStatusRow clanChallengeStatusRow, Event @event)
        {
            return $"{clanChallengeStatusRow.Rank.ToString("D2").PadRight(6)}\t{clanChallengeStatusRow.Clan.Title.PadRight(30)}\t{clanChallengeStatusRow.Score.ToString().PadRight(10)}\t{clanChallengeStatusRow.Active.ToString().PadRight(10)}\t{clanChallengeStatusRow.Total.ToString().PadRight(10)}";
        }
    }
}