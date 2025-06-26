using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using CoScheduleOA.Infrastructure.Models;
using CoScheduleOA.Interfaces.Providers;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CoScheduleOA.Infrastructure
{
    public sealed class AwsSecretsProvider : IAwsSecretsProvider
    {
        private readonly IAmazonSecretsManager _client;
        private readonly AwsSecretOptions _options;
        private AwsSecrets? _cachedSecrets;
        private readonly object _secretsLock = new();

        public AwsSecretsProvider(IAmazonSecretsManager client, IOptions<AwsSecretOptions> opts)
        {
            _client = client;
            _options = opts.Value;
        }

        private AwsSecrets LoadSecrets()
        {
            if (_cachedSecrets is not null)
                return _cachedSecrets;

            lock (_secretsLock)
            {
                if (_cachedSecrets is not null)
                    return _cachedSecrets;

                var resp = _client.GetSecretValueAsync(
                    new GetSecretValueRequest { SecretId = _options.AwsSecretId })
                    .GetAwaiter().GetResult();

                _cachedSecrets = JsonSerializer.Deserialize<AwsSecrets>(resp.SecretString!)!;
                return _cachedSecrets;
            }
        }

        public string GetPepper() => LoadSecrets().Pepper;

        public RedditSettings GetRedditCredentials()
        {
            var s = LoadSecrets();
            return new RedditSettings { 
                RedditId = s.RedditId,
                RedditPassword = s.RedditPassword,
                RedditSecretId = s.RedditSecretId,
                RedditSecretKey = s.RedditSecretKey };
        }

        public string GetJwtSecret() => LoadSecrets().JwtSecretKey;
    }
}