using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Modules;
using Ghostbot.Modules.Guardian;
using Ghostbot.Modules.Simple;

namespace Ghostbot
{
    public class Program
    {
        const string AppName = "Ghostbot";
        const string AppUrl = "https://github.com/michaeldtaylor/Ghostbot";

        DiscordClient _client;

        static void Main(string[] args) => new Program().Start();

        void Start()
        {
            _client = new DiscordClient(x =>
            {
                x.AppName = AppName;
                x.AppUrl = AppUrl;
                x.MessageCacheSize = 0;
                x.UsePermissionsCache = true;
                x.EnablePreUpdateEvents = true;
                x.LogLevel = LogSeverity.Debug;
            })
            .UsingModules()
            .UsingCommands(x =>
            {
                x.PrefixChar = '$';
                x.HelpMode = HelpMode.Public;
            });

            var modules = _client.GetService<ModuleService>();

            modules.Add(new SimpleModule(), SimpleModule.Name, ModuleFilter.None);
            modules.Add(new GuardianModule(), GuardianModule.Name, ModuleFilter.None);

            _client.ExecuteAndWait(async () => await ConnectBot());
        }

        async Task ConnectBot()
        {
            try
            {
                await _client.Connect(BotTokenProvider.GetToken()).ConfigureAwait(false);
            }
            catch (BotTokenProviderException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("The Bot token appears to be invalid.");
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
