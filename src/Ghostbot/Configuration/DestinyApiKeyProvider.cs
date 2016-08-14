namespace Ghostbot.Configuration
{
    public class DestinyApiKeyProvider
    {
        readonly IConfigurationProvider _configurationProvider;

        public DestinyApiKeyProvider(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public const string ApiKeyVariable = "DestinyApiKey";
        
        public string GetApiKey()
        {
            try
            {
                var token = _configurationProvider.GetConfigurationVariable(ApiKeyVariable);

                return token;
            }
            catch (GhostbotConfigurationException)
            {
                throw new DesktinyApiKeyProviderException();
            }
        }
    }
}