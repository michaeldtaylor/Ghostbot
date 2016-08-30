using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Discord.Commands;
using Ghostbot.Infrastructure;
using Ghostbot.Modules.ClanWars.Model;
using Ghostbot.Modules.ClanWars.View;
using HtmlAgilityPack;

namespace Ghostbot.Modules.ClanWars.Commands
{
    public class ChallengeStatusCommand : DiscordCommand
    {
        readonly Dictionary<ChallengeStatusFormat, IChallengeStatusRenderer> _challengeStatusFormatRendererMap;

        public ChallengeStatusCommand(IChallengeStatusRenderer[] challengeStatusRenderers)
        {
            AddParameter(new DiscordParameter("challengeId", ParameterType.Optional));
            AddParameter(new DiscordParameter("format", ParameterType.Optional));

            _challengeStatusFormatRendererMap = challengeStatusRenderers.ToDictionary(c => c.Format, c => c);
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

                var response = await client.GetAsync(ClanWarsApi.GetChallengeStatusRelativeUri(challengeId));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(content);

                    var contentNode = htmlDocument.GetElementbyId("content");
                    var challengeStatus = ParseChallengeStatus(challengeId, contentNode);

                    var renderer = _challengeStatusFormatRendererMap[format];
                    var renderedHeader = renderer.RenderHeader(challengeStatus);
                    var activeClansByScore = challengeStatus.Rows.Where(r => r.Active > 0).OrderByDescending(r => r.Score).ToList();

                    await args.Channel.SendMessage($"Destiny Clan Wars challenge {challengeId}:\n\n```{renderedHeader}```");

                    foreach (var activeRenderedClan in activeClansByScore.Select(r => renderer.RenderClan(r, challengeStatus.Event)))
                    {
                        await args.Channel.SendMessage($"```{activeRenderedClan}```");
                    }
                }
            }
        }

        static ChallengeStatus ParseChallengeStatus(int challengeId, HtmlNode contentNode)
        {
            return new ChallengeStatus
            {
                Id = challengeId,
                Event = ParseEvent(contentNode),
                Details = ParseChallengeDetails(contentNode),
                Rows = HtmlHelper.ParseTableRows<ClanChallengeStatusRow>(contentNode, ClanWarsApi.BaseUri)
            };
        }

        static Event ParseEvent(HtmlNode contentNode)
        {
            var headerNode = contentNode.SelectSingleNode("//h2");
            var spanNodes = headerNode.SelectNodes("span").ToArray();
            var eventLink = HtmlHelper.ParseLink(spanNodes[1], ClanWarsApi.BaseUri);

            return new Event(eventLink);
        }

        static ChallengeDetails ParseChallengeDetails(HtmlNode contentNode)
        {
            var headerNode = contentNode.SelectSingleNode("//h2");
            var spanNodes = headerNode.SelectNodes("span").ToArray();
            var issuedBy = spanNodes[0].InnerText.Split(':')[1].Trim();

            var dates = spanNodes[2].InnerText.Trim('(', ')').Split('-');
            var fromDate = dates[0].Trim();
            var toDate = dates[1].Trim();

            return new ChallengeDetails(issuedBy, fromDate, toDate);
        }
    }
}