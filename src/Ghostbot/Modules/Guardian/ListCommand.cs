using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Ghostbot.Configuration;
using Ghostbot.Domain;
using Manticore.DestinySdk.Core;
using Manticore.DestinySdk.Core.Model;

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
            var user = _discordUserRepository.Get(args.User.Mention);

            string username;
            Platform platform;

            if (user == null)
            {
                username = args.GetArg("username");
                platform = (Platform)Enum.Parse(typeof(Platform), args.GetArg("platform"));
            }
            else
            {
                username = user.DestinyUsername;
                platform = (Platform)Enum.Parse(typeof(Platform), user.DestintPlatform);
            }

            var accountSummary = await _destinyClient.GetAccountSummary(platform, username);
            var charactersCount = accountSummary.Characters.Count();

            await args.Channel.SendMessage($"{args.User.Mention} has the following number of guardians: {charactersCount}");
        }
    }
}