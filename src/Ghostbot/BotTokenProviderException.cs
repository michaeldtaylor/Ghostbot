using System;

namespace Ghostbot
{
    public class BotTokenProviderException : Exception
    {
        public BotTokenProviderException(string environmentVariable) : base($"The Ghost bot token has not been set in environment variables. Navigate to 'https://discordapp.com/developers/applications/me' to get your token. Create and set an environment variable called '{environmentVariable}'.")
        {
        }
    }
}