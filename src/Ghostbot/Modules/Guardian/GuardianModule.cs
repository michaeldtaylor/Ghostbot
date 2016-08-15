namespace Ghostbot.Modules.Guardian
{
    public class GuardianModule : DiscordModule
    {
        public GuardianModule()
        {
            AddCommand<ListCommand>();
        }

        public override string Name => "Guardian";
        protected override string Prefix => "guardian";
    }
}
