using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static DiscordDadJokeApp.Format;

namespace DiscordDadJokeApp {
    internal class Discord : IDisposable {
        internal string ChannelID { get; set; }
        internal string Webhook { get; set; }
        internal Uri DiscordWebHookUri = new("https://discord.com/api/webhooks/");
        internal string EmbedsTitle = "Hello, I'm Dad!";
        internal string EmbedsColor = "11697914";
        internal string EmbedsThumbnail = "https://i.ibb.co/HF7XL8F/pipe-removebg-preview.jpg";
        private bool disposedValue;
        private readonly HttpClient _httpClient = new();

        internal Discord(string channelID, string webhook) {
            ChannelID = channelID;
            Webhook = webhook;
        }

        internal async Task<HttpResponseMessage> SendToChatAsync() {
            try {
                Init();
                string dadJoke = await DadJoke.GetAsync(_httpClient);
                string jsonSerializedContent = SerializeBodyContent(await DadJoke.GetAsync(_httpClient));
                HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync(CreateWebhookUri(), NewPostContent(jsonSerializedContent));
                return httpResponseMessage;
            }
            catch (Exception) {
                throw;
            }
        }

        private Uri CreateWebhookUri() {
            return new Uri(DiscordWebHookUri, string.Join('/', ChannelID, Webhook));
        }

        private static StringContent NewPostContent(string content) {
            StringContent stringContent = new(content, Encoding.UTF8, "application/json");
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return stringContent;
        }

        private static HttpRequestMessage NewRequestMessage(Format.DiscordChatFormat discordChatFormat, Uri uri) {
            return new HttpRequestMessage(HttpMethod.Post, uri) {
                Content = JsonContent.Create(discordChatFormat),
            };
        }

        private void Init() {
            _httpClient.Timeout = TimeSpan.FromSeconds(5);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;
        }

        private string SerializeBodyContent(string content) {
            Embeds[] embeds = new Embeds[1];
            Thumbnail thumbnail = new() {
                url = EmbedsThumbnail
            };
            embeds[0] = new Embeds() {
                title = EmbedsTitle,
                description = content,
                color = EmbedsColor,
                thumbnail = thumbnail
            };
            DiscordChatFormat format = new() {
                embeds = embeds
            };

            return JsonSerializer.Serialize<Format.DiscordChatFormat>(format);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: dispose managed state (managed objects)
                    _httpClient.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose() {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
