using System.IO;
using Microsoft.Extensions.Configuration;

namespace DiscordDadJokeApp {
    internal sealed class AppSettings {
        public sealed class Settings {
            public string ChannelId { get; set; }
            public string Webhook { get; set; }
        }

        private const string ConfigFileName = "appsettings.json";
        private const string ConfigSection = "Discord";

        internal static Settings Get() {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(ConfigFileName, optional: false);

            IConfiguration configuration = configurationBuilder.Build();
            return configuration.GetSection(ConfigSection).Get<Settings>();
        }
    }
}
