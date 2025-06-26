using System.Text.Json.Serialization;

namespace CoScheduleOA.Infrastructure.Models
{
    public class AwsSecrets
    {
        [JsonPropertyName("pepper")]
        public string Pepper { get; set; } = null!;

        [JsonPropertyName("reddit_secret_id")]
        public string RedditSecretId { get; set; } = null!;

        [JsonPropertyName("reddit_secret_key")]
        public string RedditSecretKey { get; set; } = null!;
        [JsonPropertyName("jwt_secret_key")]
        public string JwtSecretKey { get; set; } = null!;
        [JsonPropertyName("reddit_id")]
        public string RedditId { get; set; } = null!;
        [JsonPropertyName("reddit_password")]
        public string RedditPassword { get; set; } = null!;
    }
}