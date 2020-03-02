using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Bumpy.Frontend.Configuration;
using Bumpy.Frontend.Data;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Bumpy.Frontend.Tests.Data
{
    public class BumpyQuotesClientTests
    {
        [Fact]
        public void ConstructorThrowsArgumentNullExceptionIfClientParameterIsNull()
        {
            var optionsMock = new Mock<IOptions<QuotesServiceOptions>>();
            Assert.Throws<ArgumentNullException>(() => new BumpyQuotesClient(null, optionsMock.Object));
        }

        [Fact]
        public void ConstructorThrowsArgumentNullExceptionIfOptionsParameterIsNull()
        {
            var httpClientMock = new Mock<HttpClient>();
            Assert.Throws<ArgumentNullException>(() => new BumpyQuotesClient(httpClientMock.Object,  null));
        }

        [Fact(Skip = "Unfortunately HttpClient and JsonSerializer aren't mockable. Use Flurl for sending HTTP requests.")]
        public async Task GetQuotesAsyncReturnsAllQuotes()
        {
            // Arrange
            const string quotesServiceBaseAddress = "http://example.com:12345";
            const string quotesJsonString = "[{\"id\":0,\"text\":\"Foobar\"}]";
            var streamReadCalled = 0;
            var httpClientMock = new Mock<HttpClient>();
            var httpResponseMock = new Mock<HttpResponseMessage>();
            var httpContentMock = new Mock<HttpContent>();
            var responseStreamMock = new Mock<Stream>();
            httpClientMock.Setup(c => c.GetAsync(It.IsAny<Uri>())).ReturnsAsync<HttpClient, HttpResponseMessage>(httpResponseMock.Object);
            httpResponseMock.Setup(r => r.Content).Returns(httpContentMock.Object);
            httpContentMock.Setup(c => c.ReadAsStreamAsync()).ReturnsAsync<HttpContent, Stream>(responseStreamMock.Object);

            // TODO: create an injectable wrapper for JsonSerializer.DeserializeAsync() etc. to avoid this crazy stuff like this
            responseStreamMock.Setup(s => s.Read(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns<byte[], int, int>((b, o, c) =>
                {
                    if (streamReadCalled == 0)
                    {
                        streamReadCalled++;
                        var encoding = new System.Text.UTF8Encoding();
                        return encoding.GetBytes(quotesJsonString, 0, quotesJsonString.Length, b, 0);
                    }
                    else
                    {
                        return 0;
                    }
                });

            var optionsMock = new Mock<IOptions<QuotesServiceOptions>>();
            optionsMock.Setup(o => o.Value).Returns(new QuotesServiceOptions { BaseAddress = quotesServiceBaseAddress });
            var sut = new BumpyQuotesClient(httpClientMock.Object, optionsMock.Object);

            // Act
            var result = await sut.GetAllQuotesAsync();

            // Assert
            Assert.True(result.Count > 0);
        }
    }
}
