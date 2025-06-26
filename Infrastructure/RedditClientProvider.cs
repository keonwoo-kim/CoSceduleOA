using CoScheduleOA.Interfaces.Providers;
using CoScheduleOA.Models.Reddit;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CoScheduleOA.Infrastructure
{
    public class RedditClientProvider : IRedditClientProvider
    {
        private readonly IRedditAuthProvider _auth;
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public RedditClientProvider(IRedditAuthProvider auth, HttpClient http)
        {
            _auth = auth;
            _http = http;
        }

        public async Task<RedditSearchResult> SearchAsync(string query)
        {
            var token = await _auth.GetAccessTokenAsync();
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var res = await _http.GetAsync($"search?q={Uri.EscapeDataString(query)}");
            if (!res.IsSuccessStatusCode)
            {
                var body = await res.Content.ReadAsStringAsync();
                throw new HttpRequestException(
                    $"Failed to call Reddit API: {(int)res.StatusCode} {res.StatusCode} {body}"
                );
            }

            using var stream = await res.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<RedditSearchResult>(stream, _jsonOptions)
                         ?? throw new InvalidOperationException("Reddit API response was empty or invalid");

            return result;
        }
    }
}
