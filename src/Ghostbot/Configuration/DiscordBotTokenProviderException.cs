using System;

namespace Ghostbot.Configuration
{
    public class DiscordBotTokenProviderException : Exception
    {
        public DiscordBotTokenProviderException() : base($"The Ghostbot Discord token has not been set as a user environment variable. Navigate to 'https://discordapp.com/developers/applications/me' to get your token. Create environment variable called '{DiscordBotTokenProvider.TokenVariable}' with the token value.")
        {
        }
    }
}