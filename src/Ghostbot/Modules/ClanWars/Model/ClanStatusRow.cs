using FileHelpers;

namespace Ghostbot.Modules.ClanWars.Model
{
    [DelimitedRecord(",")]
    public class ClanStatusRow
    {
        [FieldOrder(1)]
        public int Rank;
        [FieldOrder(2)]
        public string Clan;
        [FieldOrder(3)]
        public int Score;
        [FieldOrder(4)]
        public int Active;
        [FieldOrder(5)]
        public int Total;
    }
}