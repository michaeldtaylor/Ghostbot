using System.Threading.Tasks;
using Destiny.Net.Core;
using Discord.Commands;
using Ghostbot.Configuration;

namespace Ghostbot.Modules.Clan
{
    public class MembersCommand : DiscordCommand
    {
        readonly DestinyClient _destinyClient;

        public MembersCommand(DestinyApiKeyProvider destinyApiKeyProvider)
        {
            AddParameter(new DiscordParameter("name"));

            _destinyClient = new DestinyClient(destinyApiKeyProvider.GetApiKey());
        }

        protected override string Name => "members";

        protected override string Description => "Lists the members of a Clan";

        protected override async Task Execute(CommandEventArgs args)
        {
            //var bungieAccount = await _destinyClient(username, platform);
            //var renderedClan = ClanRenderer.Render(bungieAccount, platform);

            //await args.Channel.SendMessage($"```{renderedGuardian}```");
        }
    }
}