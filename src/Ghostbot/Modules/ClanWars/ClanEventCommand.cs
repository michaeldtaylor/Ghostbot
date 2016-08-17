using System.Net.Http;
using System.Threading.Tasks;
using Discord.Commands;
using Ghostbot.Infrastructure;
using Ghostbot.Modules.ClanWars.Model;
using HtmlAgilityPack;

namespace Ghostbot.Modules.ClanWars
{
    public class ClanEventCommand : DiscordCommand
    {
        public ClanEventCommand()
        {
            AddParameter(new DiscordParameter("eventId"));
            AddParameter(new DiscordParameter("clanId"));
        }

        protected override string Name => "clan-event";

        protected override string Description => "The status of a clan within a Destiny Clan Wars event";

        protected override async Task Execute(CommandEventArgs args)
        {
            var eventId = int.Parse(args.GetArg("eventId"));
            var clanId = int.Parse(args.GetArg("clanId"));
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = ClanWarsApi.BaseUri;
                client.DefaultRequestHeaders.Accept.Clear();

                var response = await client.GetAsync(ClanWarsApi.GetClanEventRelativeUri(eventId, clanId));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(content);

                    var contentNode = htmlDocument.GetElementbyId("content");

                    var statNodes = contentNode.SelectNodes("//*[contains(@class,'clan-leaderboard-stat')]");
                    var tableNode = htmlDocument.GetElementbyId("clan-member-event-results");

                    var rows = HtmlHelper.ParseTableRows<ClanMemberRow>(tableNode, ClanWarsApi.BaseUri);


                    //await args.Channel.SendMessage($"Destiny Clan Wars challenge {eventId}:\n\n```{formattedChallengeStatus}```");
                }
            }
        }
    }
}