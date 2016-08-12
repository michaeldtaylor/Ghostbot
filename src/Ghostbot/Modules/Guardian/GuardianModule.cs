namespace Ghostbot.Modules.Guardian
{
    public class GuardianModule : DiscordModule
    {
        public const string Name = "Guardians";

        public GuardianModule()
        {
            Commands.Add(new ListCommand(this));
        }

        protected override string Prefix => "guardian";
    }
}
