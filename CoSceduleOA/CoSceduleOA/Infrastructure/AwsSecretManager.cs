using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Options;
using System.Text.Json;
namespace CoScheduleOA.Infrastructure
{
    public sealed class AwsSecretManager
    {
        private readonly IAmazonSecretsManager _client;
        private readonly DbSettings _db;
        private string? _cachedConn;
        private string? _pepper;
        private readonly object _lock = new();

        public AwsSecretManager(IAmazonSecretsManager client,
                                IOptions<DbSettings> dbOpt)
        {
            _client = client;
            _db = dbOpt.Value;
        }

        public string GetPepper()
        {
            if (_pepper is not null) return _pepper;
            lock (_lock)
            {
                if (_pepper is not null) return _pepper;
                var resp = _client.GetSecretValueAsync(
                           new GetSecretValueRequest { SecretId = "pepper" })
                       .GetAwaiter().GetResult();

                var result = JsonSerializer.Deserialize<PepperSettings>(resp.SecretString!)!;
                _pepper = result.pepper;
            }
            return _pepper!;
        }
        

        public string BuildPgConnectionString(string secretId)
        {
            if (_cachedConn is not null) return _cachedConn;

            var resp = _client.GetSecretValueAsync(
                           new GetSecretValueRequest { SecretId = secretId })
                       .GetAwaiter().GetResult();

            var cred = JsonSerializer.Deserialize<DbConnectionSettings>(resp.SecretString!)!;
            _cachedConn =
                $"Host={_db.Host};Port={_db.Port};Database={_db.Name};" +
                $"Username={cred.username};Password={cred.password}";
            return _cachedConn;
        }
    }
}
