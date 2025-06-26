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
        public string Title { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string Subreddit { get; set; } = default!;
        public string Url { get; set; } = default!;
        public int Ups { get; set; } = default!;
        public int Downs { get; set; } = default!;
        public double Created { get; set; }
        public string Selftext { get; set; } = string.Empty;
        public DateTime CreatedUtc => DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(Created)).UtcDateTime;
        public string Thumbnail { get; set; } = string.Empty;
    }
}
