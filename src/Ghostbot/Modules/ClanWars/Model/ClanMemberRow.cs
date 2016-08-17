using FileHelpers;

namespace Ghostbot.Modules.ClanWars.Model
{
    [DelimitedRecord(",")]
    public class ClanMemberRow
    {
        [FieldOrder(1)]
        [FieldQuoted('"', QuoteMode.AlwaysQuoted, MultilineMode.NotAllow)]
        public string Name;
        [FieldOrder(2)]
        [FieldQuoted('"', QuoteMode.AlwaysQuoted, MultilineMode.NotAllow)]
        public string BungieId;
        [FieldOrder(3)]
        [FieldQuoted('"', QuoteMode.AlwaysQuoted, MultilineMode.NotAllow)]
        public string Console;
        [FieldOrder(4)]
        [FieldQuoted('"', QuoteMode.AlwaysQuoted, MultilineMode.NotAllow)]
        public decimal Score;
        [FieldOrder(5)]
        [FieldQuoted('"', QuoteMode.AlwaysQuoted, MultilineMode.NotAllow)]
        public int Matches;
        [FieldOrder(6)]
        [FieldQuoted('"', QuoteMode.AlwaysQuoted, MultilineMode.NotAllow)]
        public decimal PointsPerMatch;
        [FieldOrder(7)]
        [FieldQuoted('"', QuoteMode.AlwaysQuoted, MultilineMode.NotAllow)]
        public string LastPlayed;
    }
}