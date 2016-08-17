using System.Text;

namespace Ghostbot.Modules.ClanWars.Model
{
    public class ChallengeStatusWideFormatProvider : IChallengeStatusFormatProvider
    {
        public ChallengeStatusFormat Format => ChallengeStatusFormat.Wide;

        public string ApplyFormat(ChallengeStatus challengeStatus)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Issuer: {challengeStatus.Header.Issuer}");
            builder.AppendLine($"Event:  {challengeStatus.Header.Event.Title} ({challengeStatus.Header.Event.Uri})");
            builder.AppendLine($"Dates:  {challengeStatus.Header.FromDate} - {challengeStatus.Header.ToDate}");
            builder.AppendLine();
            builder.AppendLine($"{nameof(ClanChallengeStatusRow.Rank).PadRight(6)}\t{nameof(ClanChallengeStatusRow.Clan).PadRight(30)}\t{nameof(ClanChallengeStatusRow.Score).PadRight(10)}\t{nameof(ClanChallengeStatusRow.Active).PadRight(10)}\t{nameof(ClanChallengeStatusRow.Total).PadRight(10)}");
            builder.AppendLine();

            foreach (var row in challengeStatus.Rows)
            {
                builder.AppendLine($"{row.Rank.ToString("D2").PadRight(6)}\t{row.Clan.Title.PadRight(30)}\t{row.Score.ToString().PadRight(10)}\t{row.Active.ToString().PadRight(10)}\t{row.Total.ToString().PadRight(10)}");
            }

            return builder.ToString();
        }        
    }
}