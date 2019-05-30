using System;
using System.Collections.Generic;
using Bumpy.Data.Interfaces;
using Bumpy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Bumpy.Controllers
{
    [Route("bumpy/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly IQuotesRepository _quotesRepository;

        public QuotesController(IServiceProvider services)
        {
            _quotesRepository = services.GetService<IQuotesRepository>();
        }

        // GET bumpy/quotes
        [HttpGet]
        public ActionResult<IEnumerable<QuoteModel>> Get()
        {
            return Ok(_quotesRepository.GetQuotes());
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
