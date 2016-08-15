using System;
using System.Threading.Tasks;
using Discord.Commands;
using Ghostbot.Configuration;
using Ghostbot.Domain;
using Manticore.DestinySdk.Core;
using Manticore.DestinySdk.Core.Model;

namespace Ghostbot.Modules.Account
{
    public class SetCommand : DiscordCommand
    {
        readonly DestinyClient _destinyClient;
        readonly IDiscordUserRepository _discordUserRepository;

        public SetCommand(DestinyApiKeyProvider destinyApiKeyProvider, IDiscordUserRepository discordUserRepository)
        {
            AddParameter(new DiscordParameter("username"));
            AddParameter(new DiscordParameter("platform"));

            _destinyClient = new DestinyClient(destinyApiKeyProvider.GetApiKey());
            _discordUserRepository = discordUserRepository;
        }

        protected override string Name => "set";

        protected override string Description => "Sets the Destiny platform and username for a Discord user";

        protected override async Task Execute(CommandEventArgs args)
        {
            var username = args.GetArg("username");
            var platform = (Platform)Enum.Parse(typeof(Platform), args.GetArg("platform"));

            var accountSummary = await _destinyClient.GetAccountSummary(platform, username);

            if (accountSummary != null)
            {
                _discordUserRepository.Add(new DiscordUser
                {
                    DiscordId = args.User.Mention,
                    DestinyId = accountSummary.Id,
                    DestinyUsername = username,
                    DestintPlatform = platform.ToString()
                });
            }

            await args.Channel.SendMessage($"{args.User.Mention} has set their Destiny platform and username!`");
        }
    }
}