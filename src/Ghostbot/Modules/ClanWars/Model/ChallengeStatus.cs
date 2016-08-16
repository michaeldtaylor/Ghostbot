using System.Collections.Generic;
using System.Text;

namespace Ghostbot.Modules.ClanWars.Model
{
    public class ChallengeStatus
    {
        public int Id { get; set; }
        public ChallengeStatusHeader Header { get; set; }
        public IEnumerable<ClanStatusRow> Rows { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Issuer: {Header.Issuer}");
            builder.AppendLine($"Event:  {Header.EventTitle} ({Header.EventUri})");
            builder.AppendLine($"Dates:  {Header.Dates}");
            builder.AppendLine();
            builder.AppendLine($"{nameof(ClanStatusRow.Rank).PadRight(6)}\t{nameof(ClanStatusRow.Clan).PadRight(30)}\t{nameof(ClanStatusRow.Score).PadRight(10)}\t{nameof(ClanStatusRow.Active).PadRight(10)}\t{nameof(ClanStatusRow.Total).PadRight(10)}");
            builder.AppendLine();

            foreach (var row in Rows)
            {
                builder.AppendLine($"{row.Rank.ToString("D2").PadRight(6)}\t{row.Clan.PadRight(30)}\t{row.Score.ToString().PadRight(10)}\t{row.Active.ToString().PadRight(10)}\t{row.Total.ToString().PadRight(10)}");
            }

            return builder.ToString();
        }
    }
}