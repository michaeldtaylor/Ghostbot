namespace Ghostbot.Modules.Clan
{
    public class ClanModule : DiscordModule
    {
        public ClanModule()
        {
            SetConfiguration<ClanModuleConfiguration>();
            AddCommand<MembersCommand>();
        }

        public override string Name => "Clan";
        protected override string Prefix => "clan";
    }
}
