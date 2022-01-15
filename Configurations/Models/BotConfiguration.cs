namespace DiscordMusicBot.Configurations.Models
{
    using System.Text.Json.Serialization;

    public class BotConfiguration
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
