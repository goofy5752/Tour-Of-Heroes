namespace TourOfHeroesServices.Tests
{
    using Xunit;
    using System;

    public class HeroServiceTests
    {
        [Fact]
        public void ShouldReturnTrue()
        {
            var result = Math.Pow(2, 10);
            var expected = 1024;

            Assert.Equal(expected, result);
        }
    }
}
