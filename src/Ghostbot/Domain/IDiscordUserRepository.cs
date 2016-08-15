namespace Ghostbot.Domain
{
    public interface IDiscordUserRepository
    {
        DiscordUser Get(string discordId);
        void Add(DiscordUser discordUser);
    }
}