using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoScheduleOA.Infrastructure.Models.Reddit
{
    public class RedditPost
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("subreddit")]
        public string Subreddit { get; set; } = string.Empty;

        [JsonPropertyName("subreddit_id")]
        public string SubredditId { get; set; } = string.Empty;

        [JsonPropertyName("subreddit_name_prefixed")]
        public string SubredditNamePrefixed { get; set; } = string.Empty;

        [JsonPropertyName("author")]
        public string Author { get; set; } = string.Empty;

        [JsonPropertyName("author_fullname")]
        public string AuthorFullname { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("selftext")]
        public string Selftext { get; set; } = string.Empty;

        [JsonPropertyName("selftext_html")]
        public string SelftextHtml { get; set; } = string.Empty;

        [JsonPropertyName("ups")]
        public int Ups { get; set; }

        [JsonPropertyName("downs")]
        public int Downs { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("num_comments")]
        public int NumComments { get; set; }

        [JsonPropertyName("upvote_ratio")]
        public double UpvoteRatio { get; set; }

        [JsonPropertyName("over_18")]
        public bool Over18 { get; set; }

        [JsonPropertyName("is_self")]
        public bool IsSelf { get; set; }

        [JsonPropertyName("is_video")]
        public bool IsVideo { get; set; }

        [JsonPropertyName("archived")]
        public bool Archived { get; set; }

        [JsonPropertyName("locked")]
        public bool Locked { get; set; }

        [JsonPropertyName("pinned")]
        public bool Pinned { get; set; }

        [JsonPropertyName("spoiler")]
        public bool Spoiler { get; set; }

        [JsonPropertyName("quarantine")]
        public bool Quarantine { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("permalink")]
        public string Permalink { get; set; } = string.Empty;

        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; } = string.Empty;

        [JsonPropertyName("thumbnail_width")]
        public int? ThumbnailWidth { get; set; }

        [JsonPropertyName("thumbnail_height")]
        public int? ThumbnailHeight { get; set; }

        [JsonPropertyName("preview")]
        public Preview Preview { get; set; } = new Preview();

        [JsonPropertyName("created")]
        public long Created { get; set; }

        [JsonPropertyName("created_utc")]
        public long CreatedUtc { get; set; }

        [JsonPropertyName("link_flair_text")]
        public string LinkFlairText { get; set; } = string.Empty;

        [JsonPropertyName("link_flair_css_class")]
        public string LinkFlairCssClass { get; set; } = string.Empty;

        [JsonPropertyName("link_flair_richtext")]
        public List<FlairRichText> LinkFlairRichtext { get; set; }
            = new List<FlairRichText>();

        [JsonPropertyName("link_flair_text_color")]
        public string LinkFlairTextColor { get; set; } = string.Empty;

        [JsonPropertyName("link_flair_background_color")]
        public string LinkFlairBackgroundColor { get; set; } = string.Empty;

        [JsonPropertyName("author_flair_text")]
        public string AuthorFlairText { get; set; } = string.Empty;

        [JsonPropertyName("author_flair_css_class")]
        public string AuthorFlairCssClass { get; set; } = string.Empty;

        [JsonPropertyName("author_flair_richtext")]
        public List<FlairRichText> AuthorFlairRichtext { get; set; }
            = new List<FlairRichText>();

        [JsonPropertyName("media_metadata")]
        public Dictionary<string, MediaMetadata> MediaMetadata { get; set; }
            = new Dictionary<string, MediaMetadata>();

        [JsonPropertyName("gallery_data")]
        public GalleryData GalleryData { get; set; } = new GalleryData();

        [JsonPropertyName("all_awardings")]
        public List<object> AllAwardings { get; set; }
            = new List<object>();

        [JsonExtensionData]
        public Dictionary<string, JsonElement> ExtensionData { get; set; }
            = new Dictionary<string, JsonElement>();
    }

    public class FlairRichText
    {
        [JsonPropertyName("e")]
        public string E { get; set; } = string.Empty;

        [JsonPropertyName("t")]
        public string T { get; set; } = string.Empty;

        [JsonPropertyName("a")]
        public string? A { get; set; }

        [JsonPropertyName("u")]
        public string? U { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement> ExtensionData { get; set; }
            = new Dictionary<string, JsonElement>();
    }

    public class MediaMetadata
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("e")]
        public string E { get; set; } = string.Empty;

        [JsonPropertyName("m")]
        public string M { get; set; } = string.Empty;

        [JsonPropertyName("p")]
        public List<ImageSource> P { get; set; }
            = new List<ImageSource>();

        [JsonPropertyName("s")]
        public ImageSource S { get; set; } = new ImageSource();

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonExtensionData]
        public Dictionary<string, JsonElement> ExtensionData { get; set; }
            = new Dictionary<string, JsonElement>();
    }

    public class GalleryData
    {
        [JsonPropertyName("items")]
        public List<GalleryItem> Items { get; set; }
            = new List<GalleryItem>();

        [JsonExtensionData]
        public Dictionary<string, JsonElement> ExtensionData { get; set; }
            = new Dictionary<string, JsonElement>();
    }

    public class GalleryItem
    {
        [JsonPropertyName("media_id")]
        public string MediaId { get; set; } = string.Empty;

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("caption")]
        public string Caption { get; set; } = string.Empty;

        [JsonExtensionData]
        public Dictionary<string, JsonElement> ExtensionData { get; set; }
            = new Dictionary<string, JsonElement>();
    }
}
