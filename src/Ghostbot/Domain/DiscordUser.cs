using Microsoft.WindowsAzure.Storage.Table;

namespace Ghostbot.Domain
{
    public class DiscordUser : TableEntity
    {
        public const string DefaultPartitionKey = "AllDiscordUsers";

        public DiscordUser()
        {
        }

        public DiscordUser(string discordId, string destinyId, string destinyUsername, string destinyPlatform)
        {
            PartitionKey = DefaultPartitionKey;
            RowKey = discordId;
            DestinyId = destinyId;
            DestinyUsername = destinyUsername;
            DestintPlatform = destinyPlatform;
        }

        public string DiscordId => RowKey;
        public string DestinyId { get; set; }
        public string DestinyUsername { get; set; }
        public string DestintPlatform { get; set; }
    }
}