namespace DiscordDadJokeApp {
    internal class Format {
        public sealed class DadJokeFormat {
            public string id { get; set; }
            public string joke { get; set; }
            public int status { get; set; }
        }

        public sealed class DiscordChatFormat {
            public Embeds[] embeds { get; set; }
        }

        public sealed class Embeds {
            public string title { get; set; }
            public string description { get; set; }
            public string color { get; set; }
            public Thumbnail thumbnail { get; set; }
        }

        public sealed class Thumbnail {
            public string url { get; set; }
        }
    }
}
