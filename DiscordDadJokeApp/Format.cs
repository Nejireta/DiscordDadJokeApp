namespace DiscordDadJokeApp {
    internal class Format {
        public sealed class DadJokeFormat {
            public bool success { get; set; }
            public DadJokeBodyFormat[] body { get; set; }

            public sealed class DadJokeBodyFormat {
                public string _id { get; set; }
                public string setup { get; set; }
                public string punchline { get; set; }
                public string type { get; set; }
                public string[]? likes { get; set; }
                public Author author { get; set; }
                public bool approved { get; set; }
                public int date { get; set; }
                public bool NSFW { get; set; }
                public string shareableLink { get; set; }
                public sealed class Author {
                    public string name { get; set; }
                    public string? id { get; set; }
                }
            }
        }
        public sealed class DiscordChatFormat {
            public Embeds[] embeds { get; set; }
            public sealed class Embeds {
                public string title { get; set; }
                public string description { get; set; }
                public string color { get; set; }
                public Thumbnail thumbnail { get; set; }
                public sealed class Thumbnail {
                    public string url { get; set; }
                }
            }
        }
    }
}
