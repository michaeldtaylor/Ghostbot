namespace Ghostbot.Modules.ClanWars
{
    public class ClanWarsModule : DiscordModule
    {
        public ClanWarsModule()
        {
            AddCommand<StatusCommand>();
        }

        public override string Name => "Destiny Clan Wars";
        protected override string Prefix => "clanwars";
    }
}
