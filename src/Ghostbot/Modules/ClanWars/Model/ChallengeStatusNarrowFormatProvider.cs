using System.Text;

namespace Ghostbot.Modules.ClanWars.Model
{
    public class ChallengeStatusNarrowFormatProvider : IChallengeStatusFormatProvider
    {
        public ChallengeStatusFormat Format => ChallengeStatusFormat.Narrow;

        public string ApplyFormat(ChallengeStatus challengeStatus)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Issuer: {challengeStatus.Header.Issuer}");
            builder.AppendLine($"Event:  {challengeStatus.Header.Event.Title} ({challengeStatus.Header.Event.Uri})");
            builder.AppendLine($"Dates:  {challengeStatus.Header.FromDate} - {challengeStatus.Header.ToDate}");
            builder.AppendLine();

            foreach (var row in challengeStatus.Rows)
            {
                builder.AppendLine($"{row.Rank.ToString("D2")}. {row.Clan.Title}");
                builder.AppendLine($"{nameof(ClanChallengeStatusRow.Score)}:  {row.Score}");
                builder.AppendLine($"{nameof(ClanChallengeStatusRow.Active)}: {row.Active}");
                builder.AppendLine($"{nameof(ClanChallengeStatusRow.Total)}:  {row.Total}");
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}