using Xunit;
using System.Linq;
using Bumpy.Infrastructure.Data;

namespace Bumpy.Infrastructure.Tests.Data
{
    public class QuotesRepositoryTests
    {
        [Fact]
        public void GetQuotesReturnsAllQuotes()
        {
            // Arrange
            var sut = new QuotesRepository();

            // Act
            var result = sut.GetQuotes();

            // Assert
            var list = result.ToList();
            Assert.True(list.Count > 0);
        }
    }
}
