using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static DiscordDadJokeApp.Format;
using static DiscordDadJokeApp.Format.DiscordChatFormat;
using static DiscordDadJokeApp.Format.DiscordChatFormat.Embeds;

namespace DiscordDadJokeApp {
    internal class Discord : IDisposable {
        internal string ChannelID { get; set; }
        internal string Webhook { get; set; }
        internal readonly Uri DiscordWebHookUri = new("https://discord.com/api/webhooks/");
        internal const string EmbedsTitle = "Hello, I'm Dad!";
        internal const string EmbedsColor = "11697914";
        internal const string EmbedsThumbnail = "https://i.ibb.co/HF7XL8F/pipe-removebg-preview.jpg";
        private bool disposedValue;
        private readonly HttpClient _httpClient = new();

        internal Discord(string channelID, string webhook) {
            ChannelID = channelID;
            Webhook = webhook;
        }

        internal async Task<HttpResponseMessage> SendToChatAsync() {
            try {
                BuildHttpClient();
                string jsonSerializedContent = SerializeBodyContent(await DadJoke.GetAsync(_httpClient));
                using (HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync(CreateWebhookUri(), NewPostContent(jsonSerializedContent))) {
                    _ = httpResponseMessage.EnsureSuccessStatusCode();
                    return httpResponseMessage;
                }
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

        private void BuildHttpClient() {
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

            return JsonSerializer.Serialize(format);
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
