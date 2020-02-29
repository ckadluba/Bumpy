using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bumpy.Frontend.Data
{
    public class BumpyQuotesClient
    {

        private readonly HttpClient _client;

        public BumpyQuotesClient(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));

            // TODO read base uri from config Backend:BaseUri
            _client.BaseAddress = new Uri("https://localhost:44345");
        }

        public Task<List<QuoteModel>> GetAllQuotesAsync()
        {
            // TODO read this from backend API
            return Task.FromResult(new List<QuoteModel>
            {
                new QuoteModel { Id =  4, Text = "Hallo hier spricht Euronymous. Dies ist eine Grussbotschaft an alle Techno- und Housefreunde."},
                new QuoteModel { Id =  5, Text = "There is a ranch they call number 666."},
                new QuoteModel { Id =  6, Text = "In meinem Keller unter Helvete fühl' ich mich sicher. Ich kanns euch verraten."}
            });
        }
    }
}
