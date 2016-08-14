namespace Ghostbot.Modules.Guardian
{
    public class GuardiansModule : DiscordModule
    {
        public const string Name = "Guardians";

        public GuardiansModule()
        {
            AddCommand<ListCommand>();
        }

        protected override string Prefix => "guardian";
    }
}
