using Microsoft.Extensions.Configuration;

namespace DiscordDadJokeApp {
    internal sealed class ApplicationSettings {
        internal static IConfigurationRoot Get() {
            return new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
