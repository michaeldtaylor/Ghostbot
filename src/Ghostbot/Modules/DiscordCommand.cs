using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;

namespace Ghostbot.Modules
{
    public abstract class DiscordCommand
    {
        readonly List<string> _aliases = new List<string>();
        readonly List<DiscordParameter> _parameters = new List<DiscordParameter>();

        public DiscordModule Module { get; set; }

        protected abstract string Name { get; }

        protected abstract string Description { get; }
        
        protected abstract Task Execute(CommandEventArgs args);

        protected void AddAlias(string name)
        {
            _aliases.Add(name);
        }

        protected void AddParameter(DiscordParameter parameter)
        {
            _parameters.Add(parameter);
        }

        public void Register(CommandGroupBuilder commandGroupBuilder)
        {
            var commandBuilder = commandGroupBuilder.CreateCommand(Name)
                .Description(Description);
            
            if (_aliases.Count > 0)
            {
                commandBuilder.Alias(_aliases.ToArray());
            }
            
            foreach (var parameter in _parameters)
            {
                commandBuilder.Parameter(parameter.Name, parameter.ParameterType);
            }

            commandBuilder.Do(async args => await Execute(args));
        }
    }
}