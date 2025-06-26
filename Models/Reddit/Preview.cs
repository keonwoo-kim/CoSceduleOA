using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoScheduleOA.Infrastructure.Models.Reddit
{
    public class Preview
    {
        [JsonPropertyName("images")]
        public List<PreviewImage> Images { get; set; }
            = new List<PreviewImage>();

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement> ExtensionData { get; set; }
            = new Dictionary<string, JsonElement>();
    }

    public class PreviewImage
    {
        [JsonPropertyName("source")]
        public ImageSource Source { get; set; }
            = new ImageSource();

        [JsonPropertyName("resolutions")]
        public List<ImageSource> Resolutions { get; set; }
            = new List<ImageSource>();

        [JsonPropertyName("variants")]
        public Dictionary<string, object> Variants { get; set; }
            = new Dictionary<string, object>();

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonExtensionData]
        public Dictionary<string, JsonElement> ExtensionData { get; set; }
            = new Dictionary<string, JsonElement>();
    }

    public class ImageSource
    {
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement> ExtensionData { get; set; }
            = new Dictionary<string, JsonElement>();
    }
}
