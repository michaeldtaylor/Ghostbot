using System;

namespace Ghostbot
{
    public static class BotTokenProvider
    {
        const string TokenEnvironmentVariable = "GhostbotToken";
        
        public static string GetToken()
        {
            var token = Environment.GetEnvironmentVariable(TokenEnvironmentVariable, EnvironmentVariableTarget.User);

            if (string.IsNullOrEmpty(token))
            {
                throw new BotTokenProviderException(TokenEnvironmentVariable);
            }

            return token;
        }
    }
}