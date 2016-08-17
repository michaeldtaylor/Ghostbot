﻿namespace Ghostbot.Modules.ClanWars
{
    public class ClanWarsModule : DiscordModule
    {
        public ClanWarsModule()
        {
            SetConfiguration<ClanWarsModuleConfiguration>();
            AddCommand<ChallengeStatusCommand>();
            AddCommand<ClanEventLeaderboardCommand>();
        }

        public override string Name => "Destiny Clan Wars";
        protected override string Prefix => "clanwars";
    }
}
