namespace Ghostbot.Modules.Account
{
    public class AccountModule : DiscordModule
    {
        public AccountModule()
        {
            SetConfiguration<AccountModuleConfiguration>();
            AddCommand<SetCommand>();
        }

        public override string Name => "Account";
        protected override string Prefix => "account";
    }
}
