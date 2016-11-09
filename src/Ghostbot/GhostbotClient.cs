using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Discord;
using Discord.Commands;
using Discord.Modules;
using Ghostbot.Configuration;
using Ghostbot.Modules;
using NLog;

namespace Ghostbot
{
    public class GhostbotClient
    {
        static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        const string AppName = "Ghostbot";
        const string AppUrl = "https://github.com/michaeldtaylor/Ghostbot";

        DiscordClient _client;

        public void Start()
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

            RegisterActiveModules();

            _client.ExecuteAndWait(async () => await ConnectBot());
        }

        void RegisterActiveModules()
        {
            using (var moduleScope = GhostbotContainer.Current.BeginLifetimeScope())
            {
                var modules = moduleScope.Resolve<IEnumerable<IModule>>();
                var moduleService = _client.GetService<ModuleService>();

                foreach (var module in modules)
                {
                    var discordModule = module as DiscordModule;

                    if (discordModule != null && discordModule.IsActive)
                    {
                        moduleService.Add(module, discordModule.Name, discordModule.Filter);
                    }
                }
            }
        }

        async Task ConnectBot()
        {
            try
            {
                Logger.Info("Starting Ghostbot...");

                string token;

                using (var configurationScope = GhostbotContainer.Current.BeginLifetimeScope())
                {
                    var discordTokenProvider = configurationScope.Resolve<GhostbotDiscordTokenProvider>();
                    token = discordTokenProvider.GetToken();
                }

                await _client.Connect(token, TokenType.Bot).ConfigureAwait(false);
            }
            catch (GhostbotDiscordTokenProviderException ex)
            {
                Logger.Error(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("The Discord Bot token appears to be invalid.");
                Logger.Error(ex.Message);
            }
        }
    }
}