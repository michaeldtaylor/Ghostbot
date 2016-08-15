using System.Collections.Generic;
using Autofac;
using Discord.Modules;

namespace Ghostbot.Modules
{
    public abstract class DiscordModule : IModule
    {
        readonly List<DiscordCommand> _commands = new List<DiscordCommand>();

        public abstract string Name { get; }

        public virtual ModuleFilter Filter => ModuleFilter.None;

        public virtual bool IsActive => true;

        protected abstract string Prefix { get; }

        protected void AddCommand<T>() where T : DiscordCommand
        {
            using (var commandScope = Program.Container.BeginLifetimeScope())
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