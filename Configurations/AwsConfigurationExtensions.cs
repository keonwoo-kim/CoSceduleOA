using Amazon.SecretsManager;
using CoScheduleOA.Infrastructure;
using CoScheduleOA.Interfaces.Providers;

namespace CoScheduleOA.Configurations
{
    public static class AwsConfigurationExtensions
    {
        public static IServiceCollection ConfigureAwsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonSecretsManager>();
            services.AddSingleton<IAwsSecretsProvider, AwsSecretsProvider>();
            services.AddSingleton<IConnectionStringProvider, AwsPgConnectionStringProvider>();
            return services;
        }
    }
}