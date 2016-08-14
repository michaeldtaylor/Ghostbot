using System;
using System.Threading.Tasks;
using Autofac;
using Discord;
using Discord.Commands;
using Discord.Modules;
using Ghostbot.Configuration;
using Ghostbot.Infrastructure;
using Ghostbot.Modules.Account;
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

        public static IContainer Container { get; private set; }

        void Start()
        {
            Container = GhostbotContainerFactory.BuildContainer();

            SQLiteHelper.CreateDatabase();

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

            RegisterModules();

            _client.ExecuteAndWait(async () => await ConnectBot());
        }

        void RegisterModules()
        {
            var modules = _client.GetService<ModuleService>();

            using (var moduleScope = Container.BeginLifetimeScope())
            {
                modules.Add(moduleScope.Resolve<SimpleModule>(), SimpleModule.Name, ModuleFilter.None);
                modules.Add(moduleScope.Resolve<AccountModule>(), AccountModule.Name, ModuleFilter.None);
                modules.Add(moduleScope.Resolve<GuardiansModule>(), GuardiansModule.Name, ModuleFilter.None);
            }
        }

        async Task ConnectBot()
        {
            try
            {
                using (var configurationScope = Container.BeginLifetimeScope())
                {
                    var discordTokenProvider = configurationScope.Resolve<DiscordBotTokenProvider>();
                    var token = discordTokenProvider.GetBotToken();

                    await _client.Connect(token).ConfigureAwait(false);
                }
            }
            catch (DiscordBotTokenProviderException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("The Discord Bot token appears to be invalid.");
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
