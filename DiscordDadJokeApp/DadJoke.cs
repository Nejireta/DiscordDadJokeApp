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
        private static readonly HttpRequestMessage _httpRequestMessage = new();
        private static HttpResponseMessage _httpResponseMessage = new();

        internal static async Task<string> GetAsync(HttpClient httpClient) {
            try {
                BuildRequestMessage();
                _httpResponseMessage = await httpClient.SendAsync(_httpRequestMessage);
                _ = _httpResponseMessage.EnsureSuccessStatusCode();
                //DadJokeBodyFormat deserializedResponseMessage = BuildJokeString(DeserializeGetRequest(await _httpResponseMessage.Content.ReadAsStringAsync()));
                return BuildString(DeserializeGetRequest(await _httpResponseMessage.Content.ReadAsStringAsync()));
            }
            finally {
                _httpRequestMessage.Dispose();
                _httpResponseMessage.Dispose();
            }
        }

        private static DadJokeBodyFormat DeserializeGetRequest(string content) {
            return JsonSerializer.Deserialize<DadJokeFormat>(content).Body;
        }

        private static string BuildString(DadJokeBodyFormat dadJokeFormatBody) {
            StringBuilder stringBuilder = new();
            _ = stringBuilder.AppendLine(dadJokeFormatBody.Setup);
            _ = stringBuilder.AppendLine(dadJokeFormatBody.Punchline);
            return stringBuilder.ToString();
        }

        private static void BuildRequestMessage() {
            _httpRequestMessage.Method = HttpMethod.Get;
            _httpRequestMessage.RequestUri = new Uri(_configurationRoot["DadJokeURI"]);
            _httpRequestMessage.Headers.Add("X-RapidAPI-Key", _configurationRoot["RapidAPIKey"]);
            _httpRequestMessage.Headers.Add("X-RapidAPI-Host", _configurationRoot["RapidAPIHost"]);
        }
    }
}
