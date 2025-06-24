namespace CoScheduleOA.Infrastructure
{
    public class AwsSecretManagerOptions
    {
        public string Region { get; set; } = null!;
        public string SecretId { get; set; } = null!;
    }
}
