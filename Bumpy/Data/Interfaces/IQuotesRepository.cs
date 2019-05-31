using System.Collections.Generic;
using Bumpy.Models;

namespace Bumpy.Data.Interfaces
{
    public interface IQuotesRepository
    {
        ICollection<QuoteModel> GetQuotes();
        QuoteModel GetQuote(int id);
    }
}
