using System.Collections.Generic;

namespace Ghostbot.Modules.ClanWars.Model
{
    public class ChallengeStatus
    {
        public int Id { get; set; }
        public ChallengeStatusHeader Header { get; set; }
        public IEnumerable<ClanStatusRow> Rows { get; set; }
    }
}