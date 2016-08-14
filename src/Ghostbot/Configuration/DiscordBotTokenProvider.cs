namespace Ghostbot.Configuration
{
    public class DiscordBotTokenProvider
    {
        readonly IConfigurationProvider _configurationProvider;

        public DiscordBotTokenProvider(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public const string TokenVariable = "GhostbotDiscordBotToken";
        
        public string GetBotToken()
        {
            try
            {
                var token = _configurationProvider.GetConfigurationVariable(TokenVariable);

                return token;
            }
            catch (GhostbotConfigurationException)
            {
                throw new DiscordBotTokenProviderException();
            }
        }
    }
}