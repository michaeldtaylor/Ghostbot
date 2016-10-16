using System;

namespace Ghostbot.Configuration
{
    public class GhostbotDiscordTokenProviderException : Exception
    {
        public GhostbotDiscordTokenProviderException() : base($"The Ghostbot Discord token has not been set as a user environment variable. Navigate to 'https://discordapp.com/developers/applications/me' to get your token. Create environment variable called '{GhostbotDiscordTokenProvider.TokenVariable}' with the token value.")
        {
        }
    }
}