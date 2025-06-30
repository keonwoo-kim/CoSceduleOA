using Amazon;
using Amazon.SecretsManager;
using CoScheduleOA.Infrastructure;
using CoScheduleOA.Interfaces.Providers;

namespace CoScheduleOA.Configurations
{
    public static class AwsConfigurationExtensions
    {
        public static IServiceCollection ConfigureAwsServices(this IServiceCollection services, IConfiguration configuration)
        {
            var awsSection = configuration.GetSection("AWS");
            var accessKey = awsSection["AWS_ACCESS_KEY_ID"];
            var secretKey = awsSection["AWS_SECRET_ACCESS_KEY"];
            var region = awsSection["AWS_REGION"];

            if (!string.IsNullOrEmpty(accessKey) && !string.IsNullOrEmpty(secretKey))
            {
                var client = new AmazonSecretsManagerClient(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));
                services.AddSingleton<IAmazonSecretsManager>(client);
            }
            else
            {
                services.AddDefaultAWSOptions(configuration.GetAWSOptions());
                services.AddAWSService<IAmazonSecretsManager>();
            }

            services.AddSingleton<IAwsSecretsProvider, AwsSecretsProvider>();
            services.AddSingleton<IConnectionStringProvider, AwsPgConnectionStringProvider>();

            return services;
        }
    }
}