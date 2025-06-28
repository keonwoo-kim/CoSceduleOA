using CoScheduleOA.Infrastructure;
using CoScheduleOA.Infrastructure.Providers;
using CoScheduleOA.Interfaces.Providers;
using CoScheduleOA.Interfaces.Services;
using CoScheduleOA.Services;

namespace CoScheduleOA.Configurations
{
    public static class ServicesConfigurationExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.ConfigureMappers();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IRedditAuthProvider, RedditAuthProvider>();
            services.AddScoped<IRedditClientProvider, RedditClientProvider>();
            services.AddScoped<IItemService, ItemService>();
            return services;
        }
    }
}