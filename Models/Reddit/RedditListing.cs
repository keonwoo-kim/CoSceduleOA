using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoScheduleOA.Infrastructure.Models.Reddit
{
    public class RedditListing
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public ListingData Data { get; set; } = new ListingData();

        [JsonExtensionData]
        public Dictionary<string, JsonElement> ExtensionData { get; set; }
            = new Dictionary<string, JsonElement>();
    }

    public class ListingData
    {
        [JsonPropertyName("modhash")]
        public string Modhash { get; set; } = string.Empty;

        [JsonPropertyName("dist")]
        public int Dist { get; set; }

        [JsonPropertyName("facets")]
        public Dictionary<string, object> Facets { get; set; }
            = new Dictionary<string, object>();

        [JsonPropertyName("after")]
        public string? After { get; set; }

        [JsonPropertyName("before")]
        public string? Before { get; set; }

        [JsonPropertyName("children")]
        public List<RedditChild> Children { get; set; }
            = new List<RedditChild>();

        [JsonExtensionData]
        public Dictionary<string, JsonElement> ExtensionData { get; set; }
            = new Dictionary<string, JsonElement>();
    }

    public class RedditChild
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public RedditPost Data { get; set; } = new RedditPost();

        [JsonExtensionData]
        public Dictionary<string, JsonElement> ExtensionData { get; set; }
            = new Dictionary<string, JsonElement>();
    }
}
