using System.Collections.Generic;
using System.Linq;
using Bumpy.Domain;
using Bumpy.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bumpy.API.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly IQuotesRepository _quotesRepository;

        public QuotesController(IQuotesRepository quotesRepository)
        {
            _quotesRepository = quotesRepository;
        }

        // GET bumpy/quotes
        [HttpGet]
        public ActionResult<IEnumerable<QuoteModel>> Get()
        {
            return _quotesRepository.GetQuotes().ToList();
        }

        // GET bumpy/quotes/1
        [HttpGet("{id}")]
        public ActionResult<QuoteModel> Get(int id)
        {
            var quote = _quotesRepository.GetQuote(id);
            if (quote != null)
            {
                return quote;
            }

            return NotFound();
        }
    }
}
