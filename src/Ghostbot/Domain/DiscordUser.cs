using SQLite;

namespace Ghostbot.Domain
{
    public class DiscordUser
    {
        [PrimaryKey]
        public string DiscordId { get; set; }
        public string DestinyId { get; set; }
    }
}