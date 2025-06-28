namespace CoScheduleOA.Models.Items
{
    public class ItemCreateModel
    {
        public string Source { get; set; } = null!;
        public string ExternalId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
}
