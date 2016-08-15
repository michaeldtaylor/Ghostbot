using System.Linq;
using Ghostbot.Domain;

namespace Ghostbot.Infrastructure
{
    public class DiscordUserRepository : IDiscordUserRepository
    {
        public DiscordUser Get(string discordId)
        {
            return SQLiteHelper.WithConnection(c => c.Table<DiscordUser>().SingleOrDefault(u => u.DiscordId == discordId));
        }

        public void Add(DiscordUser discordUser)
        {
            var user = Get(discordUser.DiscordId);

            if (user != null)
            {
                return;
            }

            SQLiteHelper.WithConnection(c => c.Insert(discordUser));
        }
    }
}
