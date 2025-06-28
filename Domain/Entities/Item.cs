namespace CoScheduleOA.Domain.Entities
{
    public sealed class Item
    {
        public int Id { get; set; }
        public string Source { get; set; } = null!;
        public string ExternalId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Url { get; set; } = null!;
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedUtc { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; }

        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}