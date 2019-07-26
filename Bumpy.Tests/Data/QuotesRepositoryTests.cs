using System;
using Xunit;
using Bumpy.Data;
using Bumpy.Models;
using System.Linq;

namespace Bumpy.Tests
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
