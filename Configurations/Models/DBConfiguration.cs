namespace DiscordMusicBot.Configurations.Models
{
    using System.Text.Json.Serialization;

    public class DBConfiguration
    {
        [JsonPropertyName("host")]
        public string Host { get; set; }

        [JsonPropertyName("port")]
        public int Port { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("dbname")]
        public string DatabaseName { get; set; }
    }
}
