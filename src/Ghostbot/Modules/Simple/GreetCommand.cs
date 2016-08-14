using System.Threading.Tasks;
using Discord.Commands;

namespace Ghostbot.Modules.Simple
{
    public class GreetCommand : DiscordCommand
    {
        public GreetCommand()
        {
            AddParameter(new DiscordParameter("person"));
            AddAlias("hi");
        }

        protected override string Name => "greet";

        protected override string Description => "Greets a Guardian";

        protected override async Task Execute(CommandEventArgs args)
        {
            await args.Channel.SendMessage($"{args.User.Mention} greets {args.GetArg("person")}");

            //module.Client.MessageReceived += async (s, e) =>
            //{
            //    if (!e.Message.IsAuthor)
            //    {
            //        await e.Channel.SendMessage(e.Message.Text);
            //    }
            //};
        }
    }
}