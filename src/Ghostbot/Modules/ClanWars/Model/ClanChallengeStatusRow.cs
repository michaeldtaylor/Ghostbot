using FileHelpers;

namespace Ghostbot.Modules.ClanWars.Model
{
    [DelimitedRecord(",")]
    public class ClanChallengeStatusRow
    {
        [FieldOrder(1)]
        [FieldQuoted('"', QuoteMode.AlwaysQuoted, MultilineMode.NotAllow)]
        public int Rank;
        [FieldOrder(2)]
        [FieldQuoted('"', QuoteMode.AlwaysQuoted, MultilineMode.NotAllow)]
        public string ClanHtmlLink;
        [FieldOrder(3)]
        [FieldQuoted('"', QuoteMode.AlwaysQuoted, MultilineMode.NotAllow)]
        public int Score;
        [FieldOrder(4)]
        [FieldQuoted('"', QuoteMode.AlwaysQuoted, MultilineMode.NotAllow)]
        public int Active;
        [FieldOrder(5)]
        [FieldQuoted('"', QuoteMode.AlwaysQuoted, MultilineMode.NotAllow)]
        public int Total;
        public Clan Clan => Clan.Parse(ClanHtmlLink);
    }
}