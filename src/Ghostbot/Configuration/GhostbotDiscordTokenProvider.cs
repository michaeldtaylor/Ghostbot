namespace Ghostbot.Configuration
{
    public class GhostbotDiscordTokenProvider
    {
        readonly IConfigurationProvider _configurationProvider;

        public GhostbotDiscordTokenProvider(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public const string TokenVariable = "GhostbotDiscordToken";
        
        public string GetToken()
        {
            try
            {
                var token = _configurationProvider.GetConfigurationVariable(TokenVariable);

                return token;
            }
            catch (GhostbotConfigurationException)
            {
                throw new GhostbotDiscordTokenProviderException();
            }
        }
    }
}