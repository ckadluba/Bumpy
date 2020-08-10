using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace Bumpy.Frontend.Data
{
    public class BumpyQuotesClient
    {
        private readonly string _baseAddress;

        public BumpyQuotesClient(Uri baseAddress)
        {
            _baseAddress = baseAddress?.ToString()
                ?? throw new ArgumentNullException(nameof(baseAddress));
        }

        public async Task<List<QuoteModel>> GetAllQuotesAsync()
        {
            Console.WriteLine($"Sending GetAllQuotes request to {_baseAddress}");

            return await _baseAddress
                .AppendPathSegment("api")
                .AppendPathSegment("quotes")
                .GetAsync()
                .ReceiveJson<List<QuoteModel>>();
        }
    }
}
