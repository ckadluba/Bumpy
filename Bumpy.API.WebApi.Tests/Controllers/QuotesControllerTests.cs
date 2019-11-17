using System.Collections.Generic;
using Bumpy.API.WebApi.Controllers;
using Bumpy.Domain;
using Bumpy.Infrastructure.Data.Interfaces;
using Moq;
using Xunit;

namespace Bumpy.API.WebApi.Tests.Controllers
{
    public class QuotesControllerTests
    {
        [Fact]
        public void ReturnsAllQuotes()
        {
            // Arrange
            var quotesList = new List<QuoteModel> { new QuoteModel() };
            var repositoryMock = new Mock<IQuotesRepository>();
            repositoryMock.Setup(r => r.GetQuotes()).Returns(quotesList);
            var sut = new QuotesController(repositoryMock.Object);

            // Act
            var result = sut.Get();

            // Assert
            Assert.Equal(quotesList, result.Value);
        }

        [Fact]
        public void ReturnsSingleQuote()
        {
            // Arrange
            var quote = new QuoteModel();
            var repositoryMock = new Mock<IQuotesRepository>();
            repositoryMock.Setup(r => r.GetQuote(It.IsAny<int>())).Returns(quote);
            var sut = new QuotesController(repositoryMock.Object);

            // Act
            var result = sut.Get(1);

            // Assert
            Assert.Equal(quote, result.Value);
        }
    }
}