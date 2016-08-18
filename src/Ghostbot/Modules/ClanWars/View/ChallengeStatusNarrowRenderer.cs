using System.Collections.Generic;
using System.Text;
using Ghostbot.Modules.ClanWars.Model;

namespace Ghostbot.Modules.ClanWars.View
{
    public class ChallengeStatusNarrowRenderer : IChallengeStatusRenderer
    {
        public ChallengeStatusFormat Format => ChallengeStatusFormat.Narrow;
        public string RenderHeader(ChallengeStatusHeader challengeStatusHeader)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Event:  {challengeStatusHeader.Event.Title}");
            builder.AppendLine($"URL:    {challengeStatusHeader.Event.Uri}");
            builder.AppendLine($"Issuer: {challengeStatusHeader.Issuer}");
            builder.AppendLine($"From:   {challengeStatusHeader.FromDate}");
            builder.AppendLine($"To:     {challengeStatusHeader.ToDate}");
            builder.AppendLine();

            return builder.ToString();
        }

        public string RenderClans(IEnumerable<ClanChallengeStatusRow> clanChallengeStatusRows, int eventId)
        {
            var builder = new StringBuilder();

            foreach (var row in clanChallengeStatusRows)
            {
                builder.AppendLine(RenderClan(row, eventId));
            }

            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }

        public string RenderClan(ClanChallengeStatusRow clanChallengeStatusRow, int eventId)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"{clanChallengeStatusRow.Rank.ToString("D2")}. {clanChallengeStatusRow.Clan.Title}");
            builder.AppendLine($"{nameof(ClanChallengeStatusRow.Score)}:  {clanChallengeStatusRow.Score}");
            builder.AppendLine($"{nameof(ClanChallengeStatusRow.Active)}: {clanChallengeStatusRow.Active}");
            builder.AppendLine($"{nameof(ClanChallengeStatusRow.Total)}:  {clanChallengeStatusRow.Total}");

            var ghostBotCommand = GetGhostbotCommand(eventId, clanChallengeStatusRow.Clan.ClanId);

            if (ghostBotCommand != null)
            {
                builder.AppendLine();
                builder.AppendLine($"{GetGhostbotCommand(eventId, clanChallengeStatusRow.Clan.ClanId)}");
            }

            builder.AppendLine();

            return builder.ToString();
        }

        static string GetGhostbotCommand(int eventId, int clanId)
        {
            return clanId == -1 ? null : $"$clanwars clan-event-leaderboard {eventId} {clanId}";
        }
    }
}