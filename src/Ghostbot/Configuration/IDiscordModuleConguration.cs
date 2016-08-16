using Discord.Modules;

namespace Ghostbot.Configuration
{
    public interface IDiscordModuleConguration
    {
        bool IsActive { get; }
        ModuleFilter Filter { get; }
    }

    public abstract class DiscordModuleConguration : IDiscordModuleConguration
    {
        public bool IsActive { get; set; }
        public ModuleFilter Filter { get; set; }
    }
}