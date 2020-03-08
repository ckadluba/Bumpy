using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bumpy.Frontend.Configuration;
using Bumpy.Frontend.Data;
using Flurl.Http.Testing;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Bumpy.Frontend.Tests.Data
{
    public class BumpyQuotesClientTests
    {
        [Fact]
        public void ConstructorThrowsArgumentNullExceptionIfOptionsParameterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new BumpyQuotesClient(null));
        }

        [Fact]
        public async Task GetQuotesAsyncReturnsAllQuotes()
        {
            // Arrange
            const string testBaseAddress = "https://example.com:8080";
            var testResponse = new List<QuoteModel> { new QuoteModel { Id = 0, Text = "Foo" } };
            var optionsMock = new Mock<IOptions<QuotesServiceOptions>>();
            optionsMock.Setup(o => o.Value).Returns(new QuotesServiceOptions { BaseAddress = testBaseAddress });

            using var flurlTest = new HttpTest();
            flurlTest.RespondWithJson(testResponse);

            var sut = new BumpyQuotesClient(optionsMock.Object);

            // Act
            var response = await sut.GetAllQuotesAsync();

            // Assert
            Assert.True(response.Count > 0);
        }
    }
}
