using System.Collections.Generic;
using Discord.Modules;

namespace Ghostbot.Modules
{
    public abstract class DiscordModule : IModule
    {
        protected readonly List<DiscordCommand> Commands = new List<DiscordCommand>();

        protected abstract string Prefix { get; }

        public void Install(ModuleManager manager)
        {
            manager.CreateCommands(Prefix, commandGroupBuilder =>
            {
                //cgb.AddCheck();

                Commands.ForEach(c => c.Register(commandGroupBuilder));
            });
        }
    }
}