using Ghostbot.Configuration;
using Ghostbot.Domain;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Ghostbot.Infrastructure
{
    public class DiscordUserRepository : IDiscordUserRepository
    {
        const string DiscordUsersTableName = "DiscordUsers";

        readonly GhostbotAzureStorageConnectionStringProvider _ghostbotAzureStorageConnectionStringProvider;

        public DiscordUserRepository(GhostbotAzureStorageConnectionStringProvider ghostbotAzureStorageConnectionStringProvider)
        {
            _ghostbotAzureStorageConnectionStringProvider = ghostbotAzureStorageConnectionStringProvider;
        }

        public DiscordUser FindById(string discordId)
        {
            var table = GetTable();
            var retrieveOperation = TableOperation.Retrieve<DiscordUser>(DiscordUser.DefaultPartitionKey, discordId);
            var retrievedResult = table.Execute(retrieveOperation);

            return (DiscordUser)retrievedResult.Result;
        }

        public void AddOrReplace(DiscordUser discordUser)
        {
            var table = GetTable();
            var insertOperation = TableOperation.InsertOrReplace(discordUser);

            table.Execute(insertOperation);
        }

        CloudTable GetTable()
        {
            var connectionString = _ghostbotAzureStorageConnectionStringProvider.GetConnectionString();
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference(DiscordUsersTableName);

            table.CreateIfNotExists();

            return table;
        }
    }
}
