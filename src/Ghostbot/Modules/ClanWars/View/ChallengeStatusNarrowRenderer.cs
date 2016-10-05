using System.Text;
using Ghostbot.Modules.ClanWars.Model;

namespace Ghostbot.Modules.ClanWars.View
{
    public class ChallengeStatusNarrowRenderer : IChallengeStatusRenderer
    {
        public ChallengeStatusFormat Format => ChallengeStatusFormat.Narrow;

        public string RenderHeader(ChallengeStatus challengeStatus)
        {
            var header = challengeStatus.Details;
            var @event = challengeStatus.Event;
            var builder = new StringBuilder();

            builder.AppendLine($"Event:  {@event.Title}");
            builder.AppendLine($"URL:    {@event.Uri}");
            builder.AppendLine($"Issuer: {header.Issuer}");
            builder.AppendLine($"From:   {header.FromDate}");
            builder.AppendLine($"To:     {header.ToDate}");
            builder.AppendLine();

            return builder.ToString();
        }

        public string RenderClans(ChallengeStatus challengeStatus)
        {
            var builder = new StringBuilder();

            foreach (var row in challengeStatus.Rows)
            {
                builder.AppendLine(RenderClan(row, challengeStatus.Event));
            }

            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }

        public string RenderClan(ClanChallengeStatusRow clanChallengeStatusRow, Event @event)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"{clanChallengeStatusRow.Rank.ToString("D2")}. {clanChallengeStatusRow.Clan.Title}");
            builder.AppendLine($"{nameof(ClanChallengeStatusRow.Score)}:  {clanChallengeStatusRow.Score}");
            builder.AppendLine($"{nameof(ClanChallengeStatusRow.Active)}: {clanChallengeStatusRow.Active}");
            builder.AppendLine($"{nameof(ClanChallengeStatusRow.Total)}:  {clanChallengeStatusRow.Total}");

            var ghostBotCommand = GetGhostbotCommand(@event.Id, clanChallengeStatusRow.Clan.Id);

            if (ghostBotCommand != null)
            {
                builder.AppendLine();
                builder.AppendLine($"{GetGhostbotCommand(@event.Id, clanChallengeStatusRow.Clan.Id)}");
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