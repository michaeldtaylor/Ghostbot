namespace Ghostbot.Modules.Guardian
{
    public class GuardianModule : DiscordModule
    {
        public GuardianModule()
        {
            SetConfiguration<GuardianModuleConfiguration>();
            AddCommand<ListCommand>();
        }

        public override string Name => "Guardian";
        protected override string Prefix => "guardian";
    }
}
