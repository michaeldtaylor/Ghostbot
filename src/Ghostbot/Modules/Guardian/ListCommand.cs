using System;
using System.Threading.Tasks;
using Destiny.Net.Core;
using Destiny.Net.Core.Model;
using Discord.Commands;
using Ghostbot.Configuration;
using Ghostbot.Domain;
using Ghostbot.Modules.Guardian.View;

namespace Ghostbot.Modules.Guardian
{
    public class ListCommand : DiscordCommand
    {
        readonly DestinyClient _destinyClient;
        readonly IDiscordUserRepository _discordUserRepository;

        public ListCommand(DestinyApiKeyProvider destinyApiKeyProvider, IDiscordUserRepository discordUserRepository)
        {
            AddParameter(new DiscordParameter("username", ParameterType.Optional));
            AddParameter(new DiscordParameter("platform", ParameterType.Optional));

            _destinyClient = new DestinyClient(destinyApiKeyProvider.GetApiKey());
            _discordUserRepository = discordUserRepository;
        }

        protected override string Name => "list";

        protected override string Description => "Lists the Guardians of a user";

        protected override async Task Execute(CommandEventArgs args)
        {
            var discordId = args.User.Mention;
            var discordUser = _discordUserRepository.FindById(discordId);

            string username = null;
            var platform = Platform.PlayStation;

            if (discordUser != null)
            {
                username = discordUser.DestinyUsername;
                platform = (Platform)Enum.Parse(typeof(Platform), discordUser.DestintPlatform);
            }

            if (args.Args.Length == 2)
            {
                try
                {
                    username = args.GetArg("username");
                    platform = (Platform)Enum.Parse(typeof(Platform), args.GetArg("platform"));
                }
                catch (Exception)
                {
                    await args.Channel.SendMessage($"{discordId} your Destiny username or platform is missing, or platform is invalid! Please try again.");
                    return;
                }
            }

            var bungieAccount = await _destinyClient.GetBungieAccount(username, platform);
            var renderedGuardian = GuardianRenderer.Render(bungieAccount, platform);

            await args.Channel.SendMessage($"```{renderedGuardian}```");
        }
    }
}