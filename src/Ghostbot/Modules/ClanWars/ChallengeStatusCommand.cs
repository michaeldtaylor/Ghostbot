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
    public class ChallengeStatusCommand : DiscordCommand
    {
        readonly Dictionary<ChallengeStatusFormat, IChallengeStatusFormatProvider> _challengeStatusFormatProviderMap;

        public ChallengeStatusCommand(IChallengeStatusFormatProvider[] challengeStatusFormatProviders)
        {
            AddParameter(new DiscordParameter("challengeId", ParameterType.Optional));
            AddParameter(new DiscordParameter("format", ParameterType.Optional));

            _challengeStatusFormatProviderMap = challengeStatusFormatProviders.ToDictionary(c => c.Format, c => c);
        }

        protected override string Name => "challenge-status";

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
                client.BaseAddress = ClanWarsApi.BaseUri;
                client.DefaultRequestHeaders.Accept.Clear();

                var response = await client.GetAsync(ClanWarsApi.GetChallengeStatusRelativeUri(challengeId));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(content);

                    var contentNode = htmlDocument.GetElementbyId("content");
                    var challengeStatus = ParseChallengeStatus(challengeId, contentNode);

                    var formatProvider = _challengeStatusFormatProviderMap[format];
                    var formattedChallengeStatus = formatProvider.ApplyFormat(challengeStatus);

                    await args.Channel.SendMessage($"Destiny Clan Wars challenge {challengeId}:\n\n```{formattedChallengeStatus}```");
                }
            }
        }

        static ChallengeStatus ParseChallengeStatus(int challengeId, HtmlNode contentNode)
        {
            return new ChallengeStatus
            {
                Id = challengeId,
                Header = ParseChallengeStatusHeader(contentNode),
                Rows = HtmlHelper.ParseTableRows<ClanChallengeStatusRow>(contentNode, ClanWarsApi.BaseUri)
            };
        }

        static ChallengeStatusHeader ParseChallengeStatusHeader(HtmlNode contentNode)
        {
            var headerNode = contentNode.SelectSingleNode("//h2");
            var spanNodes = headerNode.SelectNodes("span").ToArray();

            var issuedBy = spanNodes[0].InnerText.Split(':')[1].Trim();
            var eventLink = HtmlHelper.ParseLink(spanNodes[1], ClanWarsApi.BaseUri);

            var dates = spanNodes[2].InnerText.Trim('(', ')').Split('-');
            var fromDate = dates[0].Trim();
            var toDate = dates[1].Trim();

            return new ChallengeStatusHeader(issuedBy, eventLink, fromDate, toDate);
        }
    }
}