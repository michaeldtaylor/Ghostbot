using System.Collections.Generic;

namespace Ghostbot.Modules.ClanWars.Model
{
    public class ClanEventLeaderboard
    {
        public int EventId { get; set; }
        public int ClanId { get; set; }
        public ClanEventLeaderboardStatistics Statistics { get; set; }
        public IEnumerable<ClanMemberRow> Rows { get; set; }
    }
}