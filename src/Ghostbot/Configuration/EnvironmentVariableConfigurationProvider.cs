using System;

namespace Ghostbot.Configuration
{
    public class EnvironmentVariableConfigurationProvider : IConfigurationProvider
    {
        public string GetConfigurationVariable(string variable)
        {
            var token = Environment.GetEnvironmentVariable(variable, EnvironmentVariableTarget.Machine);

            if (string.IsNullOrEmpty(token))
            {
                throw new GhostbotConfigurationException(variable);
            }

            return token;
        }
    }
}
