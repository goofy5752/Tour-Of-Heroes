namespace TourOfHeroesServices.Tests
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Globalization;
    using System.Collections.Generic;

    using Common;
    using Contracts;
    using TourOfHeroesData.Models;
    using TourOfHeroesData.Common.Contracts;

    using Moq;
    using Xunit;

    public class MovieServiceTests
    {
        private IMovieService _movieService;

        private List<Movie> GetMovieTestData()
        {
            return new List<Movie>()
            {
                new Movie()
                {
                    Id = 1,
                    Title = "The last air bender",
                    Hero = new Hero()
                    {
                        Id = 1
                    },
                    HeroId = 1
                },
                new Movie()
                {
                    Id = 2,
                    Title = "The last air bender2",
                    Hero = new Hero()
                    {
                        Id = 2
                    },
                    HeroId = 2
                },
            };
        }

        private List<LikedMovie> GetLikedMovieTestData()
        {
            return new List<LikedMovie>()
            {
                new LikedMovie()
                {
                    Id = 1,
                    Title = "The last air bender",
                    PosterPath = "123123123123123",
                    ReleaseDate = DateTime.Now,
                    UserId = "1",
                    VoteAverage = 12.2,
                    VoteCount = 112122,
                },
                new LikedMovie()
                {
                    Id = 2,
                    Title = "The last air bender2",
                    PosterPath = "1231231231231231212112123123213",
                    ReleaseDate = DateTime.Now,
                    UserId = "2",
                    VoteAverage = 12.22221132,
                    VoteCount = 111,
                },
            };
        }

        public MovieServiceTests()
        {
            MapperInitializer.InitializeMapper();
            Thread.Sleep(100);
        }

        [Fact]
        public void GetAllMovies_WithExistentData_ShouldReturnAllDataCorrectly()
        {
            string errorMessagePrefix = "MovieService GetAllMovies() method does not work properly.";

            var repo = new Mock<IRepository<Movie>>();
            repo.Setup(r => r.All()).Returns(GetMovieTestData().AsQueryable());

            this._movieService = new MovieService(repo.Object, null, null, null);

            var expectedResults = GetMovieTestData();
            var actualResults = this._movieService.GetAllMovies().ToList();

            Assert.True(expectedResults.Count == actualResults.Count(), errorMessagePrefix);

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Title == actualEntry.Title, errorMessagePrefix + " " + "Title is not returned properly.");
            }
        }

        [Fact]
        public void GetAllMovies_WithNonExistentData_ShouldReturnEmptyList()
        {
            string errorMessagePrefix = "MovieService GetAllMovies() method does not work properly.";

            var repo = new Mock<IRepository<Movie>>();
            repo.Setup(r => r.All()).Returns(new List<Movie>().AsQueryable());

            this._movieService = new MovieService(repo.Object, null, null, null);

            var expectedResults = 0;
            var actualResults = this._movieService.GetAllMovies().Count();

            Assert.True(expectedResults == actualResults, errorMessagePrefix);
        }

        [Fact]
        public void GetLikedMovies_WithExistentData_ShouldReturnAllDataCorrectly()
        {
            string errorMessagePrefix = "MovieService GetLikedMovies() method does not work properly.";

            var repo = new Mock<IRepository<LikedMovie>>();

            repo.Setup(r => r.All()).Returns(GetLikedMovieTestData().AsQueryable());

            this._movieService = new MovieService(null, repo.Object, null, null);

            var expectedResults = GetLikedMovieTestData().Where(x => x.UserId == "1").ToList();
            var actualResults = this._movieService.GetLikedMovies("1").ToList();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Title == actualEntry.Title, errorMessagePrefix + " " + "Title is not returned properly.");
                Assert.True(expectedEntry.PosterPath == actualEntry.PosterPath, errorMessagePrefix + " " + "PosterPath is not returned properly.");
                Assert.True(expectedEntry.ReleaseDate.ToString(CultureInfo.CurrentCulture) == actualEntry.ReleaseDate.ToString(CultureInfo.CurrentCulture), errorMessagePrefix + " " + "ReleaseDate is not returned properly.");
                Assert.True(expectedEntry.VoteAverage == double.Parse(actualEntry.VoteAverage), errorMessagePrefix + " " + "VoteAverage is not returned properly.");
                Assert.True(expectedEntry.VoteCount == int.Parse(actualEntry.VoteCount), errorMessagePrefix + " " + "Title is not returned properly.");
            }
        }

        [Fact]
        public void GetLikedMovies_WithNonExistentData_ShouldReturnEmptyList()
        {
            string errorMessagePrefix = "MovieService GetLikedMovies() method does not work properly.";

            var repo = new Mock<IRepository<LikedMovie>>();

            repo.Setup(r => r.All()).Returns(new List<LikedMovie>().AsQueryable());

            this._movieService = new MovieService(null, repo.Object, null, null);

            var expectedResults = 0;
            var actualResults = this._movieService.GetLikedMovies("1").Count();

            Assert.True(expectedResults == actualResults, errorMessagePrefix);
        }
    }
}