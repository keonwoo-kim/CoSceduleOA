using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CoScheduleOA.Infrastructure.Models;

namespace CoScheduleOA.Configurations
{
    public static class OptionsConfigurationExtensions
    {
        public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(
                configuration.GetSection("JwtSettings"));

            services.Configure<DbSettings>(
                configuration.GetSection("Database"));

            services.Configure<AwsSecretOptions>(
                configuration.GetSection("AwsSecretOptions"));

            return services;
        }
    }
}
