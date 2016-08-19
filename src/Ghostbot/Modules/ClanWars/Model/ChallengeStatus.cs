using System.Collections.Generic;

namespace Ghostbot.Modules.ClanWars.Model
{
    public class ChallengeStatus
    {
        public int Id { get; set; }
        public Event Event { get; set; }
        public ChallengeDetails Details { get; set; }
        public IEnumerable<ClanChallengeStatusRow> Rows { get; set; }
    }
}