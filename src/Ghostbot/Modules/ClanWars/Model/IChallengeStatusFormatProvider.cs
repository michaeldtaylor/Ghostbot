namespace Ghostbot.Modules.ClanWars.Model
{
    public interface IChallengeStatusFormatProvider
    {
        ChallengeStatusFormat Format { get; }
        string ApplyFormat(ChallengeStatus challengeStatus);
    }
}