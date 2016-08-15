namespace Ghostbot.Modules.Simple
{
    public class SimpleModule : DiscordModule
    {
        public SimpleModule()
        {
            AddCommand<GreetCommand>();
            AddCommand<ByeCommand>();
        }

        public override string Name => "Simple";
        public override bool IsActive => false;
        protected override string Prefix => "simple";
    }
}
