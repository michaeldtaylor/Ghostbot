using System.Collections.Generic;
using Autofac;
using Discord.Modules;
using Ghostbot.Configuration;

namespace Ghostbot.Modules
{
    public abstract class DiscordModule : IModule
    {
        readonly List<DiscordCommand> _commands = new List<DiscordCommand>();

        public abstract string Name { get; }

        public bool IsActive => Configuration.IsActive;

        public ModuleFilter Filter => Configuration.Filter;

        public IDiscordModuleConguration Configuration { get; private set; }

        protected abstract string Prefix { get; }

        protected void SetConfiguration<T>() where T : IDiscordModuleConguration
        {
            Configuration = DiscordModuleConfigurationProvider.GetModuleConfiguration<T>();
        }

        protected void AddCommand<T>() where T : DiscordCommand
        {
            using (var commandScope = GhostbotContainer.Current.BeginLifetimeScope())
            {
                var command = commandScope.Resolve<T>();
                command.Module = this;

                _commands.Add(command);
            }
        }

        public void Install(ModuleManager manager)
        {
            manager.CreateCommands(Prefix, commandGroupBuilder =>
            {
                //cgb.AddCheck();

                _commands.ForEach(c => c.Register(commandGroupBuilder));
            });
        }
    }
}