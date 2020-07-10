using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            var testBaseAddress = new Uri("https://example.com:8080");
            var testResponse = new List<QuoteModel> { new QuoteModel { Id = 0, Text = "Foo" } };

            using var flurlTest = new HttpTest();
            flurlTest.RespondWithJson(testResponse);

            var sut = new BumpyQuotesClient(testBaseAddress);

            // Act
            var response = await sut.GetAllQuotesAsync();

            // Assert
            Assert.True(response.Count > 0);
        }
    }
}
