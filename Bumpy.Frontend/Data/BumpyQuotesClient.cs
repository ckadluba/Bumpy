using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Bumpy.Frontend.Configuration;
using Microsoft.Extensions.Options;

namespace Bumpy.Frontend.Data
{
    public class BumpyQuotesClient
    {
        private readonly HttpClient _client;
        private readonly QuotesServiceOptions _options;

        public BumpyQuotesClient(HttpClient client, IOptions<QuotesServiceOptions> options)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));

            _client.BaseAddress = new Uri(_options.BaseAddress);
        }

        public async Task<List<QuoteModel>> GetAllQuotesAsync()
        {
            using var response = await _client.GetAsync("/api/quotes");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            return await JsonSerializer.DeserializeAsync<List<QuoteModel>>(responseStream, options);
        }
    }
}
