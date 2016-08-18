using System.Collections.Generic;
using Ghostbot.Modules.ClanWars.Model;

namespace Ghostbot.Modules.ClanWars.View
{
    public interface IChallengeStatusRenderer
    {
        ChallengeStatusFormat Format { get; }
        string RenderHeader(ChallengeStatusHeader challengeStatusHeader);
        string RenderClans(IEnumerable<ClanChallengeStatusRow> clanChallengeStatusRows, int eventId);
        string RenderClan(ClanChallengeStatusRow clanChallengeStatusRow, int eventId);
    }
}