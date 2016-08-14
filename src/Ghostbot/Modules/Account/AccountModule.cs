namespace Ghostbot.Modules.Account
{
    public class AccountModule : DiscordModule
    {
        public const string Name = "Account";

        public AccountModule()
        {
            AddCommand<SetCommand>();
        }

        protected override string Prefix => "account";
    }
}
