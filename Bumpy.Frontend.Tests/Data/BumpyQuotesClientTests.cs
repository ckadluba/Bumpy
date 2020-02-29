using Bumpy.Frontend.Data;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Bumpy.Frontend.Tests.Data
{
    public class BumpyQuotesClientTests
    {
        [Fact]
        public void ConstructorThrowsArgumentNullExceptionIfClientParameterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new BumpyQuotesClient(null));
        }

        [Fact]
        public async Task GetQuotesAsyncReturnsAllQuotes()
        {
            // Arrange
            var httpClientMock = new Mock<HttpClient>();
            var sut = new BumpyQuotesClient(httpClientMock.Object);

            // Act
            var result = await sut.GetAllQuotesAsync();

            // Assert
            Assert.True(result.Count > 0);
        }
    }
}
