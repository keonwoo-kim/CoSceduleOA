using CoScheduleOA.Interfaces.Providers;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoScheduleOA.Infrastructure.Providers
{
    public sealed class RedditAuthProvider : IRedditAuthProvider
    {
        private readonly HttpClient _http;
        private readonly IAwsSecretsProvider _awsSecretsProvider;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "RedditAccessToken";

        public RedditAuthProvider(HttpClient http, IAwsSecretsProvider awsSecretsProvider, IMemoryCache memoryCache)
        {
            _http = http;
            _awsSecretsProvider = awsSecretsProvider;
            _cache = memoryCache;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if (_cache.TryGetValue<string>(CacheKey, out var cachedToken) && !string.IsNullOrWhiteSpace(cachedToken))
            {
                return cachedToken;
            }

            var creds = _awsSecretsProvider.GetRedditCredentials();
            var basicAuth = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{creds.RedditSecretId}:{creds.RedditSecretKey}"));

            using var request = new HttpRequestMessage(HttpMethod.Post, "api/v1/access_token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", basicAuth);
            request.Content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", creds.RedditId),
                new KeyValuePair<string, string>("password", creds.RedditPassword)
            });

            var response = await _http.SendAsync(request);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);

            var token = doc.RootElement.GetProperty("access_token").GetString()
                ?? throw new InvalidOperationException("access_token not found");
            var expiresIn = doc.RootElement.GetProperty("expires_in").GetInt32();

            _cache.Set(CacheKey, token, TimeSpan.FromSeconds(expiresIn - 30));

            return token;
        }
    }
}