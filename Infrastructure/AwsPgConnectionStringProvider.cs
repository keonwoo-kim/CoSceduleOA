using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using CoScheduleOA.Infrastructure.Models;
using CoScheduleOA.Interfaces.Providers;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CoScheduleOA.Infrastructure
{
    public sealed class AwsPgConnectionStringProvider : IConnectionStringProvider
    {
        private readonly IAmazonSecretsManager _client;
        private readonly DbSettings _dbSettings;
        private string? _cachedConnectionString;
        private readonly object _connLock = new();

        public AwsPgConnectionStringProvider(
            IAmazonSecretsManager client,
            IOptions<DbSettings> dbOptions)
        {
            _client = client;
            _dbSettings = dbOptions.Value;
        }

        public string GetPostgresConnectionString(string secretId)
        {
            if (_cachedConnectionString is not null)
                return _cachedConnectionString;

            lock (_connLock)
            {
                if (_cachedConnectionString is not null)
                    return _cachedConnectionString;

                var resp = _client.GetSecretValueAsync(
                    new GetSecretValueRequest { SecretId = secretId })
                    .GetAwaiter().GetResult();

                var cred = JsonSerializer.Deserialize<DbConnectionSettings>(resp.SecretString!)!;

                _cachedConnectionString =
                    $"Host={_dbSettings.Host};Port={_dbSettings.Port};Database={_dbSettings.Name};" +
                    $"Username={cred.username};Password={cred.password}";

                return _cachedConnectionString;
            }
        }
    }
}
