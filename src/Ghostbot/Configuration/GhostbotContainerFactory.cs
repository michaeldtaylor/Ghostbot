using System.Reflection;
using Autofac;
using Ghostbot.Domain;
using Ghostbot.Infrastructure;

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
                .Where(t => t.Name.EndsWith("Module"))
                .AsSelf();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Command"))
                .AsSelf();

            return builder.Build();
        }
    }
}