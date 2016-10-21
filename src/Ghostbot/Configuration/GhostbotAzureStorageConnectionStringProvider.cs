namespace Ghostbot.Configuration
{
    public class GhostbotAzureStorageConnectionStringProvider
    {
        const string GhostbotAzureStorageConnectionStringVariable = "GhostbotAzureStorageConnectionString";
        
        readonly IConfigurationProvider _configurationProvider;
        
        public GhostbotAzureStorageConnectionStringProvider(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public string GetConnectionString()
        {
            return _configurationProvider.GetConfigurationVariable(GhostbotAzureStorageConnectionStringVariable);
        }
    }
}