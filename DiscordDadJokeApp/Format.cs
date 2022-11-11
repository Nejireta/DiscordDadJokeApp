namespace DiscordDadJokeApp {
    internal class Format {
        public sealed class DadJokeFormat {
            //[DisplayName("Success")]
            public bool success { get; set; } //success
            public DadJokeBodyFormat body { get; set; } //body
        }
        public sealed class DadJokeBodyFormat {
            public string _id { get; set; } //_id
            public string setup { get; set; } //setup
            public string punchline { get; set; } //punchline
            public string type { get; set; } //type
            public string[] likes { get; set; } //likes
            public AuthorSubclass author { get; set; } //author
            public bool approved { get; set; } //approved
            public int date { get; set; } //date
            public bool NSFW { get; set; } //NSFW
            public string shareableLink { get; set; } //shareableLink
        }

        public sealed class AuthorSubclass {
            public string name { get; set; } //name
            public int id { get; set; } //id
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
