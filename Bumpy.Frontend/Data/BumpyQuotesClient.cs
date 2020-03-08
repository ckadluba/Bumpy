using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bumpy.Frontend.Configuration;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace Bumpy.Frontend.Data
{
    public class BumpyQuotesClient
    {
        private readonly string _baseAddress;

        public BumpyQuotesClient(IOptions<QuotesServiceOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _baseAddress = options.Value.BaseAddress;
        }

        public Task<List<QuoteModel>> GetAllQuotesAsync() =>
            _baseAddress
                .AppendPathSegment("api")
                .AppendPathSegment("quotes")
                .GetAsync()
                .ReceiveJson<List<QuoteModel>>();
    }
}
