using System;
using System.Reflection;
using Autofac;
using Discord.Modules;
using Ghostbot.Domain;
using Ghostbot.Infrastructure;
using Ghostbot.Modules;
using Ghostbot.Modules.ClanWars.View;

namespace Ghostbot.Configuration
{
    public static class GhostbotContainer
    {
        static readonly Lazy<IContainer> LazyContainer = new Lazy<IContainer>(BuildContainer);
        
        public static IContainer Current => LazyContainer.Value;

        static IContainer BuildContainer()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();

            builder.RegisterType<EnvironmentVariableConfigurationProvider>()
                .As<IConfigurationProvider>();

            builder.RegisterType<DiscordUserRepository>()
                .As<IDiscordUserRepository>();

            builder.RegisterType<GhostbotDiscordTokenProvider>()
                .SingleInstance()
                .AsSelf();

            builder.RegisterType<GhostbotAzureStorageKeyProvider>()
                .SingleInstance()
                .AsSelf();

            builder.RegisterType<DestinyApiKeyProvider>()
                .SingleInstance()
                .AsSelf();

            builder.RegisterType<DiscordModuleConfigurationProvider>()
                .SingleInstance()
                .AsSelf();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => typeof(DiscordModule).IsAssignableFrom(t))
                .As<IModule>();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => typeof(DiscordCommand).IsAssignableFrom(t))
                .AsSelf();

            // TODO: Module specific
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => typeof(IChallengeStatusRenderer).IsAssignableFrom(t))
                .As<IChallengeStatusRenderer>();

            return builder.Build();
        }
    }
}