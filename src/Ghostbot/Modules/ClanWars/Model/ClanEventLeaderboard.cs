using System.Collections.Generic;

namespace Ghostbot.Modules.ClanWars.Model
{
    public class ClanEventLeaderboard
    {
        public Event Event { get; set; }
        public Clan Clan { get; set; }
        public ClanEventLeaderboardStatistics Statistics { get; set; }
        public IEnumerable<ClanMemberRow> Rows { get; set; }
    }
}