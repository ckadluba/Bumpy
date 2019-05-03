﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bumpy.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bumpy.Controllers
{
    [Route("bumpy/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly QuoteModel[] _quotes;

        public QuotesController()
        {
            _quotes = new QuoteModel[]
            {
                new QuoteModel { Id =  1, Text = "Hallo hier spricht Ilsa Gold. Dies ist eine Grussbotschaft an alle Techno- und Housefreunde."},
                new QuoteModel { Id =  2, Text = "There is a ranch they call number 51."},
                new QuoteModel { Id =  3, Text = "In meinem Zimmer unter dem Garten fühl' ich mich sicher. Ich kanns euch verraten."}
            };
        }

        // GET bumpy/quotes
        [HttpGet]
        public ActionResult<IEnumerable<QuoteModel>> Get()
        {
            return _quotes;
        }

        // GET bumpy/quotes/1
        [HttpGet("{id}")]
        public ActionResult<QuoteModel> Get(int id)
        {
            var quote = _quotes.SingleOrDefault(q => q.Id == id);
            if (quote != null)
            {
                return quote;
            }

            return NotFound();
        }
    }
}
