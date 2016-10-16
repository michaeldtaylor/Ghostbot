namespace Ghostbot.Configuration
{
    public class GhostbotAzureStorageKeyProvider
    {
        readonly IConfigurationProvider _configurationProvider;

        public GhostbotAzureStorageKeyProvider(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public const string GhostbotAzureStorageKeyVariable = "GhostbotAzureStorageKey";

        public string GetStorageKey()
        {
            return _configurationProvider.GetConfigurationVariable(GhostbotAzureStorageKeyVariable);
        }
    }
}