using System.Threading.Tasks;
using Discord.Commands;

namespace Ghostbot.Modules.Guardian
{
    public class ListCommand : DiscordCommand
    {
        public ListCommand(DiscordModule module) : base(module)
        {
            AddParameter(new DiscordParameter("username"));
            AddParameter(new DiscordParameter("network"));
        }

        protected override string Name => "list";

        protected override string Description => "Lists the Guardians of a user";

        protected override async Task Execute(CommandEventArgs args)
        {
            await args.Channel.SendMessage($"{args.User.Mention} says goodbye to {args.GetArg("person")}");
        }
    }
}