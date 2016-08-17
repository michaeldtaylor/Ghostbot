using System;
using FileHelpers;
using Ghostbot.Infrastructure;

namespace Ghostbot.Modules.ClanWars.Model
{
    [DelimitedRecord(",")]
    public class ClanMemberRow
    {
        [FieldOrder(1)]
        [FieldQuoted('"', QuoteMode.AlwaysQuoted, MultilineMode.NotAllow)]
        public string NameHtmlLink;
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

        public string Name => HtmlLink.Parse(NameHtmlLink).Title;

        public string LastPlayedText
        {
            get
            {
                var dateTime = DateTime.Parse(LastPlayed);

                if (DateTime.Today.Date == dateTime)
                {
                    return "Today";
                }

                return DateTime.Today.Date.AddDays(-1) == dateTime ? "Yesterday" : LastPlayed;
            }
        } 
    }
}