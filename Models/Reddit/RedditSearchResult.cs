using CoScheduleOA.Infrastructure.Models.Reddit;
using System.Text.Json.Serialization;

namespace CoScheduleOA.Models.Reddit
{
    public class RedditSearchResult
    {
        public Listing Data { get; set; } = new();
    }

    public class Listing
    {
        public string? After { get; set; }
        public List<Child> Children { get; set; } = new();
    }

    public class Child
    {
        public string Kind { get; set; } = default!;
        public PostData Data { get; set; } = new();
    }

    public class PostData
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string Subreddit { get; set; } = default!;
        public string Url { get; set; } = default!;
        public int Ups { get; set; } = default!;
        public int Downs { get; set; } = default!;
        public double Created { get; set; }
        public string Selftext { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public Preview? Preview { get; set; }
        [JsonPropertyName("gallery_data")]
        public GalleryData? GalleryData { get; set; }

        [JsonPropertyName("media_metadata")]
        public Dictionary<string, MediaMetadata>? MediaMetadata { get; set; }

        public DateTime CreatedUtc => DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(Created)).UtcDateTime;
    }

    public class Preview
    {
        public List<PreviewImage> Images { get; set; } = new();
    }

    public class PreviewImage
    {
        public PreviewSource Source { get; set; } = new();
    }

    public class PreviewSource
    {
        public string Url { get; set; } = string.Empty;
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class GalleryData
    {
        [JsonPropertyName("items")]
        public List<GalleryItem> Items { get; set; } = new();
    }

    public class GalleryItem
    {
        [JsonPropertyName("media_id")]
        public string MediaId { get; set; } = string.Empty;

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

    public class MediaMetadata
    {
        [JsonPropertyName("s")]
        public MediaSource S { get; set; } = new();
    }

    public class MediaSource
    {
        [JsonPropertyName("u")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("x")]
        public int Width { get; set; }

        [JsonPropertyName("y")]
        public int Height { get; set; }
    }
}
