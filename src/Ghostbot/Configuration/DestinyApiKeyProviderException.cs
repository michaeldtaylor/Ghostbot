using System;

namespace Ghostbot.Configuration
{
    public class DestinyApiKeyProviderException : Exception
    {
        public DestinyApiKeyProviderException() : base($"Your Destiny API key token has not been set as a user environment variable. Create environment variable called '{DestinyApiKeyProvider.ApiKeyVariable}' with the token value.")
        {
        }
    }
}