using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace DiscordDadJokeApp {
    public class DiscordDadJoke {
        private readonly AppSettings.Settings _configuration = AppSettings.Get();
        //private readonly string _channelId = "1035625089590378526";
        //private readonly string _webhook = "DtNNNZIjbu4kgKTiE-8_zMz13kY0evLHw8GMNgW_h5Nyq3PljJkGypmRD-FZ9rKH4SlB";

        [FunctionName("SendDadJoke")]
        public async Task RunAsync([TimerTrigger("0 0 14 * * *")] TimerInfo myTimer, ILogger log) {
            Discord discord = new(_configuration.ChannelId, _configuration.Webhook);
            try {
                HttpResponseMessage httpResponseMessage = await discord.SendToChatAsync();

                log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
                log.LogInformation($"Sent chat to Dad bot with reponse: {httpResponseMessage.StatusCode}");
            }
            catch(Exception ex) {
                log.LogError(ex.ToString());
            }
            finally {
                discord.Dispose();
            }
        }
    }
}
