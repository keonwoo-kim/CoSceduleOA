namespace CoScheduleOA.Infrastructure.Models
{
    public class AwsSecretOptions
    {
        public string Region { get; set; } = null!;
        public string DbSecretId { get; set; } = null!;
        public string AwsSecretId { get; set; } = null!;
    }
}
