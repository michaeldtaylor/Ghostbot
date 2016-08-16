namespace Ghostbot.Modules.Simple
{
    public class SimpleModule : DiscordModule
    {
        public SimpleModule()
        {
            SetConfiguration<SimpleModuleConfiguration>();
            AddCommand<GreetCommand>();
            AddCommand<ByeCommand>();
        }

        public override string Name => "Simple";
        protected override string Prefix => "simple";
    }
}
