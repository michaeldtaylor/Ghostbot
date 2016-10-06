namespace Ghostbot.Domain
{
    public interface IDiscordUserRepository
    {
        DiscordUser FindById(string discordId);
        void Add(DiscordUser discordUser);
    }
}