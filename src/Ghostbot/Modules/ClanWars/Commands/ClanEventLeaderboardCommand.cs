using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Ghostbot.Infrastructure;
using Ghostbot.Modules.ClanWars.Model;
using Ghostbot.Modules.ClanWars.View;
using HtmlAgilityPack;

namespace Ghostbot.Modules.ClanWars.Commands
{
    public class ClanEventLeaderboardCommand : DiscordCommand
    {
        static int PageSize => 10;

        public ClanEventLeaderboardCommand()
        {
            AddParameter(new DiscordParameter("eventId"));
            AddParameter(new DiscordParameter("clanId"));
        }

        protected override string Name => "clan-event-leaderboard";

        protected override string Description => "The leaderboard of a clan within a Destiny Clan Wars event";

        protected override async Task Execute(CommandEventArgs args)
        {
            var eventId = int.Parse(args.GetArg("eventId"));
            var clanId = int.Parse(args.GetArg("clanId"));

            using (var client = new HttpClient())
            {
                client.BaseAddress = ClanWarsApi.BaseUri;

                var response = await client.GetAsync(ClanWarsApi.GetClanEventStatusRelativeUri(eventId, clanId));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(content);

                    var contentNode = htmlDocument.GetElementbyId("content");
                    var headerNodes = HtmlHelper.GetElementsByClass(contentNode, "contentBoxHeader");
                    var eventDetailsNode = HtmlHelper.GetElementsByClass(contentNode, "event_description")[0];
                    var tooltipScriptNode = contentNode.SelectSingleNode("script");
                    var clanNameNode = headerNodes.Last();
                    var statisticsNodes = HtmlHelper.GetElementsByClass(contentNode, "clan-leaderboard-stat");
                    var tableNode = htmlDocument.GetElementbyId("clan-member-event-results");

                    var @event = ParseEvent(eventId, eventDetailsNode, tooltipScriptNode);
                    var clan = ParseClan(clanId, clanNameNode);
                    var statistics = ParseStatistics(statisticsNodes);
                    var rows = HtmlHelper.ParseTableRows<ClanMemberRow>(tableNode, ClanWarsApi.BaseUri);

                    var clanEventLeaderboard = new ClanEventLeaderboard
                    {
                        Event = @event,
                        Clan = clan,
                        Statistics = statistics,
                        Rows = rows
                    };

                    var renderedStatistics = ClanEventLeaderboardRenderer.RenderStatistics(clanEventLeaderboard);

                    await args.Channel.SendMessage($"Destiny Clan Wars event {eventId} leaderboard for clan {clanEventLeaderboard.Clan.Title} ({clanId}):\n\n```{renderedStatistics}```");

                    var activeMembersByScore = clanEventLeaderboard.Rows.Where(r => r.Score > 0).OrderByDescending(r => r.Score).ToList();
                    var renderedEvent = ClanEventLeaderboardRenderer.RenderEvent(clanEventLeaderboard);

                    await args.Channel.SendMessage($"```{renderedEvent}```");
                    await PageClanMemberRows(PageSize, activeMembersByScore, args.Channel);
                }
            }
        }

        static async Task PageClanMemberRows(int pageSize, ICollection<ClanMemberRow> rows, Channel channel)
        {
            var pages = (int)Math.Ceiling(rows.Count / (decimal)pageSize);

            for (var i = 0; i < pages; i++)
            {
                var startingIndex = pageSize * i;
                var currentPage = rows.Skip(startingIndex).Take(pageSize);
                var renderedClanMembers = ClanEventLeaderboardRenderer.RenderClanMembers(startingIndex, currentPage);

                await channel.SendMessage($"```{renderedClanMembers}```");
            }
        }

        static Event ParseEvent(int eventId, HtmlNode eventDetailsNode, HtmlNode tooltipScriptNode)
        {
            var headerNode = eventDetailsNode.SelectSingleNode("//h4");
            var headerParts = headerNode.InnerHtml.Trim().Split(new[] { "<span>", "</span>" }, StringSplitOptions.RemoveEmptyEntries);
            var eventModifierNodes = HtmlHelper.GetElementsByClass(eventDetailsNode, "event_modifier_icon");

            var tooltipScriptParts = tooltipScriptNode.InnerHtml.Trim().Split(new[] { "$('#", "').tooltipsy({", "});" }, StringSplitOptions.RemoveEmptyEntries);
            var tooltips = new Dictionary<string, Tooltip>();

            for (var i = 0; i < tooltipScriptParts.Length; i += 2)
            {
                var contentPart = tooltipScriptParts[i + 1].Trim().Split(new[] { "content: " }, StringSplitOptions.RemoveEmptyEntries)[0];
                var tooltipParts = contentPart.Split(new[] { "'<h3 class=\"tool-head\">", "</h3>", "<div class=\"tool-body\">", "</div>" }, StringSplitOptions.RemoveEmptyEntries);

                var tooltip = new Tooltip
                {
                    Id = tooltipScriptParts[i],
                    Header = tooltipParts[0],
                    Body = tooltipParts[1]
                };

                tooltips.Add(tooltip.Id, tooltip);
            }

            var title = headerParts[0];
            var description = headerParts[1];
            var modifers = eventModifierNodes.Select(e =>
            {
                var imgNode = e.SelectSingleNode("img");
                var imgId = imgNode.Attributes["id"].Value;
                var tooltip = tooltips[imgId];

                return new Modifier(tooltip.Header, tooltip.Body, e.InnerText);
            });

            return new Event(eventId, title, description, modifers);
        }

        static Clan ParseClan(int clanId, HtmlNode clanNameNode)
        {
            return new Clan(clanNameNode.InnerText.Split(':')[1].Trim(), new Uri(ClanWarsApi.BaseUri, ClanWarsApi.GetClanRelativeUri(clanId)));
        }

        static ClanEventLeaderboardStatistics ParseStatistics(HtmlNodeCollection statisticsNodes)
        {
            var statistics = statisticsNodes.Select(ParseStatisticsNode).ToList();

            var mostMatches = statistics[0];
            var mostPoints = statistics[1];
            var highestKd = statistics[2];
            var highestWinPercentage = statistics[3];
            var highestPointsPerMatch = statistics[4];
            var pewPew = statistics.Count > 6 ? statistics[5] : null;
            var orbs = statistics.Count > 7 ? statistics[6] : null;

            return new ClanEventLeaderboardStatistics(mostMatches, mostPoints, highestKd, highestWinPercentage, highestPointsPerMatch, pewPew, orbs);
        }

        static LeaderboardElement ParseStatisticsNode(HtmlNode statisticsNode)
        {
            var statisticsParts = statisticsNode.InnerHtml.Trim().Split(new[] { "<span>", "</span>" }, StringSplitOptions.RemoveEmptyEntries);
            var playerResultParts = statisticsParts[1].Split(new[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries);

            return new LeaderboardElement
            {
                Player = playerResultParts[0],
                Result = playerResultParts[1]
            };
        }
    }

    class Tooltip
    {
        public string Id { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
    }
}