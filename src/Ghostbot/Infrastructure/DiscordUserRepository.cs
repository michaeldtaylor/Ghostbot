using System.Linq;
using Ghostbot.Domain;

namespace Ghostbot.Infrastructure
{
    public class DiscordUserRepository : IDiscordUserRepository
    {
        public string GetDestinyId(string discordId)
        {
            using (var connection = SQLiteHelper.GetConnection())
            {
                var user = connection.Table<DiscordUser>().SingleOrDefault(u => u.DiscordId == discordId);

                return user?.DestinyId;
            }
        }

        public void Add(DiscordUser discordUser)
        {
            var user = GetDestinyId(discordUser.DiscordId);

            if (user != null)
            {
                return;
            }

            using (var connection = SQLiteHelper.GetConnection())
            {
                connection.Insert(discordUser);
            }
        }
    }
}
