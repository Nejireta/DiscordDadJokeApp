using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using static DiscordDadJokeApp.Format;
using static DiscordDadJokeApp.Format.DadJokeFormat;

namespace DiscordDadJokeApp {
    internal sealed class DadJoke {
        private static readonly IConfigurationRoot _configurationRoot = ApplicationSettings.Get();

        internal static async Task<string> GetAsync(HttpClient httpClient) {
            try {
                using (HttpRequestMessage httpRequestMessage = BuildHttpRequestMessage()) {
                    using (HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage)) {
                        _ = httpResponseMessage.EnsureSuccessStatusCode();
                        return BuildString(DeserializeGetRequest(await httpResponseMessage.Content.ReadAsStringAsync()));
                    }
                }
            }
            catch (Exception) {
                throw;
            }
        }

        private static DadJokeBodyFormat DeserializeGetRequest(string content) {
            return JsonSerializer.Deserialize<DadJokeFormat>(content).body.FirstOrDefault();
        }

        private static string BuildString(DadJokeBodyFormat dadJokeFormatBody) {
            StringBuilder stringBuilder = new();
            _ = stringBuilder.AppendLine(dadJokeFormatBody.setup);
            _ = stringBuilder.AppendLine();
            _ = stringBuilder.AppendLine(dadJokeFormatBody.punchline);
            return stringBuilder.ToString();
        }

        private static HttpRequestMessage BuildHttpRequestMessage() {
            HttpRequestMessage httpRequestMessage = new() {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_configurationRoot["DadJokeURI"])
            };
            httpRequestMessage.Headers.Add("X-RapidAPI-Host", _configurationRoot["RapidAPIHost"]);
            httpRequestMessage.Headers.Add("X-RapidAPI-Key", _configurationRoot["RapidAPIKey"]);
            return httpRequestMessage;
        }
    }
}
