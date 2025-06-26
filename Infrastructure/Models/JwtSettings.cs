namespace CoScheduleOA.Infrastructure.Models
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public int ExpiresMin { get; set; }
    }
}
