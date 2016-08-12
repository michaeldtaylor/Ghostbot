using Discord.Commands;

namespace Ghostbot.Modules
{
    public class DiscordParameter
    {
        public DiscordParameter(string name, ParameterType parameterType = ParameterType.Required)
        {
            Name = name;
            ParameterType = parameterType;
        }

        public string Name { get; }
        public ParameterType ParameterType { get; }
    }
}