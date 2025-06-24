namespace CoScheduleOA.Domain
{
    public sealed class Comment
    {
        public int Id { get; set; }
        public string ItemId { get; set; } = default!;
        public int UserId { get; set; }
        public string Content { get; set; } = default!;
        public DateTime CreatedUtc { get; set; }
        public DateTime UpdatedUtc { get; set; }
    }
}
