namespace CoScheduleOA.Domain.Entities
{
    public sealed class Comment
    {
        public int Id { get; set; }
        public int ItemId { get; set; } = default!;
        public int UserId { get; set; }
        public string Content { get; set; } = default!;
        public DateTime CreatedUtc { get; set; }
        public DateTime UpdatedUtc { get; set; }
        public bool IsDeleted { get; set; }

        public User User { get; set; } = default!;
        public Item Item { get; set; } = default!;
    }
}
