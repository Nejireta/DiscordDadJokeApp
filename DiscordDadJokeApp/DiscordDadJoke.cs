using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DiscordDadJokeApp {
    public class DiscordDadJoke {
        private readonly IConfigurationRoot _configurationRoot = ApplicationSettings.Get();

        [FunctionName("SendDadJoke")]
        public async Task RunAsync([TimerTrigger("0 0 14 * * *")] TimerInfo myTimer, ILogger log) {
            try {
                using (Discord discord = new(_configurationRoot["DiscordChannelID"], _configurationRoot["DiscordWebhook"])) {
                    using (HttpResponseMessage httpResponseMessage = await discord.SendToChatAsync()) {
                        if (httpResponseMessage.IsSuccessStatusCode) {
                            log.LogInformation($"Successfully sent message to Dad Bot. Response StatusCode: {httpResponseMessage.StatusCode}");
                        }
                        else {
                            log.LogInformation($"Failed sending message to Dad Bot. Response StatusCode: {httpResponseMessage.StatusCode}");
                        }
                    }
                }
            }
            catch (Exception ex) {
                log.LogError(ex.ToString());
            }
        }
    }
}
