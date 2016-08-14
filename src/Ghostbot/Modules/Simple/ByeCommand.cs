using System.Threading.Tasks;
using Discord.Commands;

namespace Ghostbot.Modules.Simple
{
    public class ByeCommand : DiscordCommand
    {
        public ByeCommand()
        {
            AddParameter(new DiscordParameter("person"));
            AddAlias("bb");
        }

        protected override string Name => "bye";

        protected override string Description => "Says goodbye to a Guardian";

        protected override async Task Execute(CommandEventArgs args)
        {
            await args.Channel.SendMessage($"{args.User.Mention} says goodbye to {args.GetArg("person")}");
        }
    }
}