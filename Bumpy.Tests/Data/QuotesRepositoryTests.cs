using System;
using Xunit;
using Bumpy.Data;
using Bumpy.Models;

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
            var list = sut.GetQuotes();

            // Assert
            Assert.True(list.Count > 0);
        }
    }
}
