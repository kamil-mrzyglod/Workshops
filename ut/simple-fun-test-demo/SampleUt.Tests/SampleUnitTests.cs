using System;
using Xunit;

namespace SampleUt.Tests
{
    public class SimpleUnitTests
    {
        [Fact]
        public void should_return_two_for_one_plus_one()
        {
            Assert.Equal(2, 1 + 1);
        }

        [Theory]
        [InlineData(10, 5, 5)]
        [InlineData(10, 6, 4)]
        public void should_add_numbers(int expected, int l, int r)
        {
            var sum = l + r;
            Assert.Equal(expected, sum);
        }
    }
}
