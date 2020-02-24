namespace TourOfHeroesServices.Tests.Common
{
    using System;

    using TourOfHeroesData;

    using Microsoft.EntityFrameworkCore;

    public static class TourOfHeroesDbContextInMemoryFactory
    {
        public static TourOfHeroesDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<TourOfHeroesDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new TourOfHeroesDbContext(options);
        }
    }
}