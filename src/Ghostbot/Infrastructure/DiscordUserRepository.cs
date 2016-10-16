using Ghostbot.Configuration;
using Ghostbot.Domain;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace Ghostbot.Infrastructure
{
    public class DiscordUserRepository : IDiscordUserRepository
    {
        const string StorageAccountName = "ghostbot";
        const string DiscordUsersTableName = "discordusers";

        readonly GhostbotAzureStorageKeyProvider _ghostbotAzureStorageKeyProvider;

        public DiscordUserRepository(GhostbotAzureStorageKeyProvider ghostbotAzureStorageKeyProvider)
        {
            _ghostbotAzureStorageKeyProvider = ghostbotAzureStorageKeyProvider;
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
            var storageKey = _ghostbotAzureStorageKeyProvider.GetStorageKey();
            var storageAccount = new CloudStorageAccount(new StorageCredentials(StorageAccountName, storageKey), true);
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference(DiscordUsersTableName);

            table.CreateIfNotExists();

            return table;
        }
    }
}
