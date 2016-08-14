namespace Ghostbot.Domain
{
    public interface IDiscordUserRepository
    {
        string GetDestinyId(string discordId);
        void Add(DiscordUser discordUser);
    }
}