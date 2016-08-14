using System;

namespace Ghostbot.Configuration
{
    public class GhostbotConfigurationException : Exception
    {
        public GhostbotConfigurationException(string variable) : base($"The Ghostbot variable '{variable}' has not been set in environment variables")
        {
        }
    }
}