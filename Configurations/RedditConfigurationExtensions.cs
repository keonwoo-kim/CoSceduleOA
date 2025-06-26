using CoScheduleOA.Infrastructure;
using CoScheduleOA.Infrastructure.Providers;
using CoScheduleOA.Interfaces.Providers;
using System.Net.Http.Headers;
using System.Text;

namespace CoScheduleOA.Configurations
{
    public static class RedditConfigurationExtensions
    {
        public static IServiceCollection AddRedditClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();

            services.AddHttpClient<IRedditAuthProvider, RedditAuthProvider>()
                .ConfigureHttpClient((sp, client) =>
                {
                    client.BaseAddress = new Uri("https://www.reddit.com/");

                    var aws = sp.GetRequiredService<IAwsSecretsProvider>();
                    var creds = aws.GetRedditCredentials();
                    var userAgent = string.Format("{0}:{1} (by /u/{2})",
                        configuration["Reddit:UserAgent"],
                        configuration["Reddit:Version"],
                        creds.RedditId);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", userAgent);

                    var basicAuth = Convert.ToBase64String(
                        Encoding.UTF8.GetBytes($"{creds.RedditSecretId}:{creds.RedditSecretKey}"));
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Basic", basicAuth);
                });

            // API 호출용 HttpClient: BaseAddress, User-Agent
            services.AddHttpClient<IRedditClientProvider, RedditClientProvider>()
                .ConfigureHttpClient((sp, client) =>
                {
                    client.BaseAddress = new Uri("https://oauth.reddit.com/");
                    var aws = sp.GetRequiredService<IAwsSecretsProvider>();
                    var creds = aws.GetRedditCredentials();
                    var userAgent = string.Format("{0}:{1} (by /u/{2})",
                        configuration["Reddit:UserAgent"],
                        configuration["Reddit:Version"],
                        creds.RedditId);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", userAgent);
                });

            return services;
        }
    }
}