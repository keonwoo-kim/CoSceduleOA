namespace CoScheduleOA.Interfaces.Providers
{
    public interface IRedditAuthProvider
    {
        Task<string> GetAccessTokenAsync();
    }
}
