using Ghostbot.Domain;

namespace Ghostbot.Infrastructure
{
    public class DiscordUserRepository : IDiscordUserRepository
    {
        public DiscordUser FindById(string discordId)
        {
            //return SQLiteHelper.WithConnection(c => c.Table<DiscordUser>().SingleOrDefault(u => u.DiscordId == discordId));
            return new DiscordUser();
        }

        public void Add(DiscordUser discordUser)
        {
            var user = FindById(discordUser.DiscordId);

            if (user != null)
            {
                return;
            }

            //SQLiteHelper.WithConnection(c => c.Insert(discordUser));
        }
    }
}
