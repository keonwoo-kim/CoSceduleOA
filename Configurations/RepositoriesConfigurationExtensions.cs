using CoScheduleOA.Interfaces.Repositories;
using CoScheduleOA.Repositories;

namespace CoScheduleOA.Configurations
{
    public static class RepositoriesConfigurationExtensions
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();

            return services;
        }
    }
}