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

namespace Ghostbot.Modules.ClanWars
{
    public class ClanEventLeaderboardCommand : DiscordCommand
    {
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
                    var statisticsNodes = HtmlHelper.GetElementsByClass(contentNode, "clan-leaderboard-stat");
                    var tableNode = htmlDocument.GetElementbyId("clan-member-event-results");

                    var clanEventLeaderboard = new ClanEventLeaderboard
                    {
                        EventId = eventId,
                        ClanId = clanId,
                        Statistics = ParseStatistics(statisticsNodes),
                        Rows = HtmlHelper.ParseTableRows<ClanMemberRow>(tableNode, ClanWarsApi.BaseUri)
                    };

                    var renderedStatistics = ClanEventLeaderboardRenderer.RenderStatistics(clanEventLeaderboard.Statistics);

                    await args.Channel.SendMessage($"Destiny Clan Wars event {eventId} leaderboard for clan {clanId}:\n\n```{renderedStatistics}```");

                    var activeMembersByScore = clanEventLeaderboard.Rows.Where(r => r.Score > 0).OrderByDescending(r => r.Score).ToList();

                    await PageClanMemberRows(10, activeMembersByScore, args.Channel);
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

        static ClanEventLeaderboardStatistics ParseStatistics(HtmlNodeCollection statisticsNodes)
        {
            var statistics = statisticsNodes.Select(ParseStatisticsNode).ToList();

            var mostMatches = statistics[0];
            var mostPoints = statistics[1];
            var highestKd = statistics[2];
            var highestWinPercentage = statistics[3];
            var highestPointsPerMatch = statistics[4];
            var pewPew = statistics[5];
            var orbs = statistics[6];

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

    public class LeaderboardElement
    {
        public string Player { get; set; }
        public string Result { get; set; }
    }
}