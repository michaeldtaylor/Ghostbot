namespace Ghostbot.Modules.Simple
{
    public class SimpleModule : DiscordModule
    {
        public const string Name = "Simple";

        public SimpleModule()
        {
            AddCommand<GreetCommand>();
            AddCommand<ByeCommand>();
        }

        protected override string Prefix => "simple";
    }
}
