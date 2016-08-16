using System.Reflection;
using Autofac;
using Discord.Modules;
using Ghostbot.Domain;
using Ghostbot.Infrastructure;
using Ghostbot.Modules;
using Ghostbot.Modules.ClanWars.Model;

namespace Ghostbot.Configuration
{
    public static class GhostbotContainerFactory
    {
        public static IContainer BuildContainer()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();

            builder.RegisterType<EnvironmentVariableConfigurationProvider>()
                .As<IConfigurationProvider>();

            builder.RegisterType<DiscordUserRepository>()
                .As<IDiscordUserRepository>();

            builder.RegisterType<DiscordBotTokenProvider>()
                .SingleInstance()
                .AsSelf();

            builder.RegisterType<DestinyApiKeyProvider>()
                .SingleInstance()
                .AsSelf();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => typeof(DiscordModule).IsAssignableFrom(t))
                .As<IModule>();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => typeof(DiscordCommand).IsAssignableFrom(t))
                .AsSelf();

            // TODO: Be inside module
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => typeof(IChallengeStatusFormatProvider).IsAssignableFrom(t))
                .As<IChallengeStatusFormatProvider>();

            return builder.Build();
        }
    }
}