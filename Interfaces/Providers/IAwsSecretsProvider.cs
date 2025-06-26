using CoScheduleOA.Infrastructure.Models;

namespace CoScheduleOA.Interfaces.Providers
{
    public interface IAwsSecretsProvider
    {
        /// <summary>
        /// Retrieves the pepper value stored in AWS Secrets Manager.
        /// </summary>
        string GetPepper();

        /// <summary>
        /// Retrieves Reddit OAuth credentials from AWS Secrets Manager.
        /// </summary>
        RedditSettings GetRedditCredentials();

        /// <summary>
        /// Retrieves the Jwt value stored in AWS Secrets Manager.
        /// </summary>
        string GetJwtSecret();
    }
}