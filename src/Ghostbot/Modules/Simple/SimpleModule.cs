namespace Ghostbot.Modules.Simple
{
    public class SimpleModule : DiscordModule
    {
        public const string Name = "Simple";

        public SimpleModule()
        {
            Commands.Add(new GreetCommand(this));
            Commands.Add(new ByeCommand(this));
        }

        protected override string Prefix => "simple";
    }
}
