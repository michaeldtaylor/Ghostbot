namespace Ghostbot.Configuration
{
    public interface IConfigurationProvider
    {
        string GetConfigurationVariable(string variable);
    }
}