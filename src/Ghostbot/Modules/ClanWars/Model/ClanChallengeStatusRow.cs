using FileHelpers;

namespace Ghostbot.Modules.ClanWars.Model
{
    [DelimitedRecord(",")]
    public class ClanChallengeStatusRow
    {
        [FieldOrder(1)]
        public int Rank { get; set; }
        [FieldOrder(2)]
        public string ClanHtmlLink { get; set; }
        [FieldOrder(3)]
        public int Score { get; set; }
        [FieldOrder(4)]
        public int Active { get; set; }
        [FieldOrder(5)]
        public int Total { get; set; }
        public Clan Clan => Clan.Parse(ClanHtmlLink);
    }
}