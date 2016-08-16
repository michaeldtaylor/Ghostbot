using System;
using System.Collections.Generic;
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
        static readonly Uri ClanWarsBaseUri = new Uri("http://destinyclanwars.com");

        readonly Dictionary<ChallengeStatusFormat, IChallengeStatusFormatProvider> _challengeStatusFormatProviderMap;
        
        public StatusCommand(IChallengeStatusFormatProvider[] challengeStatusFormatProviders)
        {
            AddParameter(new DiscordParameter("challengeId", ParameterType.Optional));
            AddParameter(new DiscordParameter("format", ParameterType.Optional));

            _challengeStatusFormatProviderMap = challengeStatusFormatProviders.ToDictionary(c => c.Format, c => c);
        }

        protected override string Name => "status";

        protected override string Description => "The current status of a Destiny Clan Wars challenge";

        protected override async Task Execute(CommandEventArgs args)
        {
            var challengeIdArg = args.GetArg("challengeId");
            var formatArg = args.GetArg("format");

            var challengeId = ((ClanWarsModuleConfiguration)Module.Configuration).DefaultChallengeId;
            var format = ((ClanWarsModuleConfiguration)Module.Configuration).DefaultStatusFormat;

            if (!string.IsNullOrEmpty(challengeIdArg))
            {
                challengeId = int.Parse(challengeIdArg);
            }

            if (!string.IsNullOrEmpty(formatArg))
            {
                format = (ChallengeStatusFormat)Enum.Parse(typeof(ChallengeStatusFormat), formatArg);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = ClanWarsBaseUri;
                client.DefaultRequestHeaders.Accept.Clear();

                var response = await client.GetAsync($"/challenges/view/{challengeId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(content);

                    var contentNode = htmlDocument.GetElementbyId("content");
                    var challengeStatus = ParseChallengeStatus(challengeId, contentNode);

                    var formatProvider = _challengeStatusFormatProviderMap[format];
                    var formattedChallengeStatus = formatProvider.ApplyFormat(challengeStatus);

                    await args.Channel.SendMessage($"Clan Wars status for challenge {challengeId}:\n\n```{formattedChallengeStatus}```");
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
            var eventUri = new Uri(ClanWarsBaseUri, eventAnchorNode.Attributes[0].Value);
            var eventTitle = eventAnchorNode.InnerText;

            var dates = spanNodes[2].InnerText.Trim('(', ')').Split('-');
            var fromDate = dates[0].Trim();
            var toDate = dates[1].Trim();

            return new ChallengeStatusHeader(issuedBy, eventUri, eventTitle, fromDate, toDate);
        }
    }
}