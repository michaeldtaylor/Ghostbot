using System;
using System.Threading.Tasks;
using Destiny.Net.Core;
using Destiny.Net.Core.Model;
using Discord.Commands;
using Ghostbot.Configuration;
using Ghostbot.Domain;

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
            var discordId = args.User.Mention;

            string username;
            Platform platform;

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

            var destinyPlayerResponse = await _destinyClient.SearchDestinyPlayer(platform, username);

            if (destinyPlayerResponse == null)
            {
                await args.Channel.SendMessage($"{discordId} failed to find your Destiny username and platform! Please try again.");
                return;
            }

            var discordUser = new DiscordUser(discordId, destinyPlayerResponse.MembershipId, username, platform.ToString());

            _discordUserRepository.AddOrReplace(discordUser);

            await args.Channel.SendMessage($"{discordId} set their Destiny username and platform!");
        }
    }
}