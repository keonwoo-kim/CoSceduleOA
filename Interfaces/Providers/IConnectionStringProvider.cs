namespace CoScheduleOA.Interfaces.Providers
{
    public interface IConnectionStringProvider
    {
        string GetPostgresConnectionString(string secretId);
    }
}
