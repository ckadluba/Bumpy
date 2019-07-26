using System.Collections.Generic;
using System.Linq;
using Bumpy.Data.Interfaces;
using Bumpy.Models;

namespace Bumpy.Data
{
    public class QuotesRepository : IQuotesRepository
    {
        private readonly List<QuoteModel> _quotes;

        public QuotesRepository()
        {
            _quotes = new List<QuoteModel>
            {
                new QuoteModel { Id =  1, Text = "Hallo hier spricht Ilsa Gold. Dies ist eine Grussbotschaft an alle Techno- und Housefreunde."},
                new QuoteModel { Id =  2, Text = "There is a ranch they call number 51."},
                new QuoteModel { Id =  3, Text = "In meinem Zimmer unter dem Garten fühl' ich mich sicher. Ich kanns euch verraten."}
            };
        }

        public IEnumerable<QuoteModel> GetQuotes() => _quotes;

        public QuoteModel GetQuote(int id)
        {
            var quote = _quotes.SingleOrDefault(q => q.Id == id);
            if (quote != null)
            {
                return quote;
            }

            return null;
        }
    }
}