using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Discord.Commands;
using Ghostbot.Infrastructure;
using Ghostbot.Modules.ClanWars.Model;
using HtmlAgilityPack;

namespace Ghostbot.Modules.ClanWars
{
    public class StatusCommand : DiscordCommand
    {
        public const int DefaultChallengeId = 142;
        public const string ClanWarsBaseUri = "http://destinyclanwars.com";

        public StatusCommand()
        {
            AddParameter(new DiscordParameter("challengeId", ParameterType.Optional));
        }

        protected override string Name => "status";

        protected override string Description => "The current status of a Clan Wars challenge";

        protected override async Task Execute(CommandEventArgs args)
        {
            var challengeId = DefaultChallengeId;
            var challengeIdArg = args.GetArg("challengeId");

            if (!string.IsNullOrEmpty(challengeIdArg))
            {
                challengeId = int.Parse(challengeIdArg);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ClanWarsBaseUri);
                client.DefaultRequestHeaders.Accept.Clear();

                var response = await client.GetAsync($"/challenges/view/{challengeId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(content);

                    var contentNode = htmlDocument.GetElementbyId("content");
                    var clanStatus = ParseChallengeStatus(challengeId, contentNode);

                    await args.Channel.SendMessage($"Clan Wars status for challenge {challengeId}:\n\n```{clanStatus}```");
                }
            }
        }

        static ChallengeStatus ParseChallengeStatus(int challengeId, HtmlNode contentNode)
        {
            return new ChallengeStatus
            {
                Id = challengeId,
                Header = ParseChallengeStatusHeader(contentNode),
                Rows = HtmlTableParser.ParseTableRows<ClanStatusRow>(contentNode)
            };
        }

        static ChallengeStatusHeader ParseChallengeStatusHeader(HtmlNode contentNode)
        {
            var headerNode = contentNode.SelectSingleNode("//h2");
            var spanNodes = headerNode.SelectNodes("span").ToArray();

            var issuedBy = spanNodes[0].InnerText.Trim().Split(':')[1].Trim();

            var eventAnchorNode = spanNodes[1].SelectSingleNode("a");
            var eventUri = new Uri(ClanWarsBaseUri + eventAnchorNode.Attributes[0].Value);
            var eventTitle = eventAnchorNode.InnerText;

            var dates = spanNodes[2].InnerText.Trim();

            return new ChallengeStatusHeader(issuedBy, eventUri, eventTitle, dates);
        }
    }
}