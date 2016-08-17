using System.IO;

namespace Ghostbot.Configuration
{
    public class DiscordModuleConfigurationProvider
    {
        const string ConfigurationFileName = "configuration.json";
        static readonly string ConfigurationFilePath = Path.Combine(AssemblyHelper.AssemblyDirectory, ConfigurationFileName);

        public T GetModuleConfiguration<T>() where T : IDiscordModuleConguration
        {
            return ConfigurationReader.Read<T>(ConfigurationFilePath);
        }
    }
}