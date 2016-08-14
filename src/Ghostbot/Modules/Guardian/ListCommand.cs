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
            AddParameter(new DiscordParameter("username"));
            AddParameter(new DiscordParameter("platform"));

            _destinyClient = new DestinyClient(destinyApiKeyProvider.GetApiKey());
            _discordUserRepository = discordUserRepository;
        }

        protected override string Name => "list";

        protected override string Description => "Lists the Guardians of a user";

        protected override async Task Execute(CommandEventArgs args)
        {
            var username = args.GetArg("username");
            var platform = (Platform)Enum.Parse(typeof(Platform), args.GetArg("platform"));

            var accountSummary = await _destinyClient.GetAccountSummary(platform, username);
            var charactersCount = accountSummary.Characters.Count();

            await args.Channel.SendMessage($"{args.User.Mention} has the following number of guardians: {charactersCount}");
        }
    }
}