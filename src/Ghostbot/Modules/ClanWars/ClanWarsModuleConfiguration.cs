using Ghostbot.Configuration;
using Ghostbot.Modules.ClanWars.Model;

namespace Ghostbot.Modules.ClanWars
{
    public class ClanWarsModuleConfiguration : DiscordModuleConguration
    {
        public int DefaultChallengeId { get; set; }
        public ChallengeStatusFormat DefaultStatusFormat { get; set; }
    }
}
