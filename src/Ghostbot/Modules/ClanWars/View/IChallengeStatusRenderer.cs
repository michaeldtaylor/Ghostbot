using Ghostbot.Modules.ClanWars.Model;

namespace Ghostbot.Modules.ClanWars.View
{
    public interface IChallengeStatusRenderer
    {
        ChallengeStatusFormat Format { get; }
        string RenderHeader(ChallengeStatus challengeStatus);
        string RenderClans(ChallengeStatus challengeStatus);
        string RenderClan(ClanChallengeStatusRow clanChallengeStatusRow, Event @event);
    }
}