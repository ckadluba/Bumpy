using System.Collections.Generic;
using Bumpy.Domain;

namespace Bumpy.Infrastructure.Data.Interfaces
{
    public interface IQuotesRepository
    {
        IEnumerable<QuoteModel> GetQuotes();
        QuoteModel GetQuote(int id);
    }
}
