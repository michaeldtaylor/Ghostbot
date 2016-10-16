namespace Ghostbot.Domain
{
    public interface IDiscordUserRepository
    {
        DiscordUser FindById(string discordId);
        void AddOrReplace(DiscordUser discordUser);
    }
}