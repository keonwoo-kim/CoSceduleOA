namespace CoScheduleOA.Domain
{
    public sealed class Rating
    {
        public int Id { get; set; }
        public string ItemId { get; set; } = default!;
        public int UserId { get; set; }
        public short Value { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime UpdatedUtc { get; set; }
    }
}
