using System.Text.Json.Serialization;

namespace CoScheduleOA.Infrastructure.Models
{
    public class RedditSettings
    {
        public string RedditSecretId { get; set; } = null!;
        public string RedditSecretKey { get; set; } = null!;
        public string RedditId { get; set; } = null!;
        public string RedditPassword { get; set; } = null!;
    }
}
