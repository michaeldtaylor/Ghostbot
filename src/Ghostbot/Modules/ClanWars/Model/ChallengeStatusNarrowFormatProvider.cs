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
            builder.AppendLine($"Event:  {challengeStatus.Header.EventTitle} ({challengeStatus.Header.EventUri})");
            builder.AppendLine($"Dates:  {challengeStatus.Header.Dates}");
            builder.AppendLine();

            foreach (var row in challengeStatus.Rows)
            {
                builder.AppendLine($"{row.Rank.ToString("D2")}. {row.Clan}");
                builder.AppendLine($"{nameof(ClanStatusRow.Score)}:  {row.Score}");
                builder.AppendLine($"{nameof(ClanStatusRow.Active)}: {row.Active}");
                builder.AppendLine($"{nameof(ClanStatusRow.Total)}:  {row.Total}");
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}