using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace DiscordDadJokeApp {
    public class DiscordDadJoke {
        private readonly string _channelId = Environment.GetEnvironmentVariable("DiscordChannelID"); //Added to Application settings in Function app
        private readonly string _webhook = Environment.GetEnvironmentVariable("DiscordWebhook"); //Added to Application settings in Function app
        //private readonly string _channelId = "1035625089590378526";
        //private readonly string _webhook = "DtNNNZIjbu4kgKTiE-8_zMz13kY0evLHw8GMNgW_h5Nyq3PljJkGypmRD-FZ9rKH4SlB";

        [FunctionName("SendDadJoke")]
        public async Task RunAsync([TimerTrigger("0 0 14 * * *")] TimerInfo myTimer, ILogger log) {
            Discord discord = new(_channelId, _webhook);
            try {
                HttpResponseMessage httpResponseMessage = await discord.SendToChatAsync();
                if (httpResponseMessage.IsSuccessStatusCode) {
                    log.LogInformation($"Successfully sent message to Dad Bot\nResponse StatusCode: {httpResponseMessage.StatusCode}");
                }
                else {
                    log.LogInformation($"Failed sending message to Dad Bot\nResponse StatusCode: {httpResponseMessage.StatusCode}");
                }
            }
            catch (Exception ex) {
                log.LogError(ex.ToString());
            }
            finally {
                discord.Dispose();
            }
        }
    }
}
