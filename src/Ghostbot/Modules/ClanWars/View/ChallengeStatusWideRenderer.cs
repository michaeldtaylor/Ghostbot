using System.Collections.Generic;
using System.Text;
using Ghostbot.Modules.ClanWars.Model;

namespace Ghostbot.Modules.ClanWars.View
{
    public class ChallengeStatusWideRenderer : IChallengeStatusRenderer
    {
        public ChallengeStatusFormat Format => ChallengeStatusFormat.Wide;

        public string RenderHeader(ChallengeStatusHeader challengeStatusHeader)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Issuer: {challengeStatusHeader.Issuer}");
            builder.AppendLine($"Event:  {challengeStatusHeader.Event.Title} ({challengeStatusHeader.Event.Uri})");
            builder.AppendLine($"Dates:  {challengeStatusHeader.FromDate} - {challengeStatusHeader.ToDate}");
            builder.AppendLine();
            builder.AppendLine($"{nameof(ClanChallengeStatusRow.Rank).PadRight(6)}\t{nameof(ClanChallengeStatusRow.Clan).PadRight(30)}\t{nameof(ClanChallengeStatusRow.Score).PadRight(10)}\t{nameof(ClanChallengeStatusRow.Active).PadRight(10)}\t{nameof(ClanChallengeStatusRow.Total).PadRight(10)}");

            return builder.ToString();
        }

        public string RenderClans(IEnumerable<ClanChallengeStatusRow> clanChallengeStatusRows, int eventId)
        {
            var builder = new StringBuilder();

            foreach (var row in clanChallengeStatusRows)
            {
                builder.AppendLine(RenderClan(row, eventId));
            }

            return builder.ToString();
        }

        public string RenderClan(ClanChallengeStatusRow clanChallengeStatusRow, int eventId)
        {
            return $"{clanChallengeStatusRow.Rank.ToString("D2").PadRight(6)}\t{clanChallengeStatusRow.Clan.Title.PadRight(30)}\t{clanChallengeStatusRow.Score.ToString().PadRight(10)}\t{clanChallengeStatusRow.Active.ToString().PadRight(10)}\t{clanChallengeStatusRow.Total.ToString().PadRight(10)}";
        }
    }
}