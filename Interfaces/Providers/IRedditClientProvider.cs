using CoScheduleOA.Models.Reddit;

namespace CoScheduleOA.Interfaces.Providers
{
    public interface IRedditClientProvider
    {
        Task<RedditSearchResult> SearchAsync(string query);
    }
}
