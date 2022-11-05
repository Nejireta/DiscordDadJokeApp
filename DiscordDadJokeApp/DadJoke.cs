using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiscordDadJokeApp {
    internal sealed class DadJoke {
        private static readonly Uri _dadJokeUri = new(@"https://icanhazdadjoke.com/");

        internal static async Task<string> GetAsync(HttpClient httpClient) {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(_dadJokeUri);
            _ = httpResponseMessage.EnsureSuccessStatusCode();
            return DeserializeGetRequest(await httpResponseMessage.Content.ReadAsStringAsync());
        }

        private static string DeserializeGetRequest(string content) {
            return JsonSerializer.Deserialize<Format.DadJokeFormat>(content).joke;

        }
    }
}
