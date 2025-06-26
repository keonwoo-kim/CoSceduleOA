namespace CoScheduleOA.Models.Ratings
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string UserName { get; set; } = null!;
        public short Value { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime UpdatedUtc { get; set; }
    }
}