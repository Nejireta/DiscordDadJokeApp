using System;
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
            using (HttpRequestMessage httpRequestMessage = NewRequestMessage()) {
                using (HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage)) {
                    _ = httpResponseMessage.EnsureSuccessStatusCode();
                    DadJokeBodyFormat deserializedResponseMessage = DeserializeGetRequest(await httpResponseMessage.Content.ReadAsStringAsync());
                    return BuildJokeString(deserializedResponseMessage);
                }
            }
        }

        private static DadJokeBodyFormat DeserializeGetRequest(string content) {
            return JsonSerializer.Deserialize<DadJokeFormat>(content).Body;
        }

        private static string BuildJokeString(DadJokeBodyFormat dadJokeFormatBody) {
            StringBuilder stringBuilder = new();
            _ = stringBuilder.AppendLine(dadJokeFormatBody.Setup);
            _ = stringBuilder.AppendLine(dadJokeFormatBody.Punchline);
            return stringBuilder.ToString();
        }

        private static HttpRequestMessage NewRequestMessage() {
            HttpRequestMessage httpRequestMessage = new() {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_configurationRoot["DadJokeURI"])
            };
            httpRequestMessage.Headers.Add("X-RapidAPI-Key", _configurationRoot["RapidAPIKey"]);
            httpRequestMessage.Headers.Add("X-RapidAPI-Host", _configurationRoot["RapidAPIHost"]);
            return httpRequestMessage;
        }
    }
}
