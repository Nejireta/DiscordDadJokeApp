namespace DiscordDadJokeApp {
    internal class Format {
        public sealed class DadJokeFormat {
            //[DisplayName("Success")]
            public bool Success { get; set; } //success
            public DadJokeBodyFormat Body { get; set; } //body
            public sealed class DadJokeBodyFormat {
                public string Id { get; set; } //_id
                public string Setup { get; set; } //setup
                public string Punchline { get; set; } //punchline
                public string Type { get; set; } //type
                public string[] Likes { get; set; } //likes
                public AuthorSubclass Author { get; set; } //author
                public bool Approved { get; set; } //approved
                public int Date { get; set; } //date
                public bool NSFW { get; set; } //NSFW
                public string ShareableLink { get; set; } //shareableLink

                public sealed class AuthorSubclass {
                    public string Name { get; set; } //name
                    public int Id { get; set; } //id
                }
            }
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
