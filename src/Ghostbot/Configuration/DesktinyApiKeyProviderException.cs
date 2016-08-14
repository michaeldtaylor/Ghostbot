using System;

namespace Ghostbot.Configuration
{
    public class DesktinyApiKeyProviderException : Exception
    {
        public DesktinyApiKeyProviderException() : base($"Your Destiny API key token has not been set as a user environment variable. Create environment variable called '{DestinyApiKeyProvider.ApiKeyVariable}' with the token value.")
        {
        }
    }
}