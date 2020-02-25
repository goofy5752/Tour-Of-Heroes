using System.Threading;

namespace TourOfHeroesServices.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Common;
    using Contracts;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.HeroDtos;
    using TourOfHeroesData.Common.Contracts;

    using Moq;
    using Xunit;

    using Microsoft.AspNetCore.Http;

    public class HeroServiceTests
    {
        private IHeroService _heroService;

        private List<Hero> GetTestData()
        {
            return new List<Hero>
            {
                new Hero()
                {
                    Id = 1,
                    Name = "Cpt America",
                    RealName = "Go6o",
                    Image = "n3sht0.si",
                    CoverImage = "ne6to.s1",
                    Birthday = DateTime.Now,
                    Description = "ne znam ni6to za nego",
                    Gender = "Muj",
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            HeroId = 1
                        },
                        new Comment()
                        {
                            HeroId = 1
                        }
                    },
                    EditHistory = new List<EditHistory>()
                    {
                        new EditHistory()
                        {
                            HeroId = 1
                        }
                    },
                    Movies = new List<Movie>()
                    {
                        new Movie()
                        {
                            HeroId = 1,
                            Title = "The last air bender"
                        },
                        new Movie()
                        {
                            HeroId = 1,
                            Title = "The last air bender2"
                        }
                    }
                },
                new Hero()
                {
                    Id = 2,
                    Name = "Cpt America2",
                    RealName = "Go6o2",
                    Image = "n3sht0.si2",
                    CoverImage = "ne6to.s12",
                    Birthday = DateTime.Now,
                    Description = "ne znam ni6to za nego2",
                    Gender = "Muj2",
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            HeroId = 2
                        },
                        new Comment()
                        {
                            HeroId = 2
                        }
                    },
                    EditHistory = new List<EditHistory>()
                    {
                        new EditHistory()
                        {
                            HeroId = 2
                        }
                    }
                }
            };
        }

        public HeroServiceTests()
        {
            MapperInitializer.InitializeMapper();
            Thread.Sleep(11);
        }

        [Fact]
        public void GetAllHeroes_WithCorrectData_ShouldReturnListWithAllHeroes()
        {
            string errorMessagePrefix = "HeroService GetAllUsers() method does not work properly.";

            var repo = new Mock<IRepository<Hero>>();
            repo.Setup(r => r.All()).Returns(GetTestData().AsQueryable());

            this._heroService = new HeroService(repo.Object, null, null, null);

            var expectedResults = GetTestData();
            var actualResults = this._heroService.GetAllHeroes().ToList();

            Assert.True(expectedResults.Count == actualResults.Count(), errorMessagePrefix);

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Name == actualEntry.Name, errorMessagePrefix + " " + "Email is not returned properly.");
                Assert.True(expectedEntry.RealName == actualEntry.RealName, errorMessagePrefix + " " + "RealName is not returned properly.");
                Assert.True(expectedEntry.Image == actualEntry.Image, errorMessagePrefix + " " + "Image is not returned properly.");
            }
        }

        [Fact]
        public void GetAllHeroes_WithEmptyList_ShouldReturnEmptyList()
        {
            string errorMessagePrefix = "HeroService GetAllUsers() method does not work properly.";

            var repo = new Mock<IRepository<Hero>>();
            repo.Setup(r => r.All()).Returns(new List<Hero>(0).AsQueryable);

            this._heroService = new HeroService(repo.Object, null, null, null);

            var expectedResults = 0;
            var actualResults = this._heroService.GetAllHeroes().ToList();

            Assert.True(expectedResults == actualResults.Count, errorMessagePrefix);
        }

        [Fact]
        public void GetById_WithExistentId_ShouldReturnCorrectHeroDetail()
        {
            string errorMessagePrefix = "HeroService GetById() method does not work properly.";

            var repo = new Mock<IRepository<Hero>>();
            var userRepo = new Mock<IRepository<ApplicationUser>>();
            repo.Setup(r => r.All()).Returns(this.GetTestData().AsQueryable);
            userRepo.Setup(x => x.All()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "1"
                }
            }.AsQueryable);

            this._heroService = new HeroService(repo.Object, null, userRepo.Object, null);

            var expectedResults = this.GetTestData().FirstOrDefault(x => x.Id == 1);
            var actualResults = this._heroService.GetById("1", 1);

            Assert.True(expectedResults.Name == actualResults.Name, errorMessagePrefix + " " + "Email is not returned properly.");
            Assert.True(expectedResults.RealName == actualResults.RealName, errorMessagePrefix + " " + "RealName is not returned properly.");
            Assert.True(expectedResults.Image == actualResults.Image, errorMessagePrefix + " " + "Image is not returned properly.");
            Assert.True(expectedResults.CoverImage == actualResults.CoverImage, errorMessagePrefix + " " + "CoverImage is not returned properly.");
            Assert.True(expectedResults.Gender == actualResults.Gender, errorMessagePrefix + " " + "Gender is not returned properly.");
            Assert.True(expectedResults.Description == actualResults.Description, errorMessagePrefix + " " + "Description is not returned properly.");
            Assert.True(expectedResults.Birthday.ToString(CultureInfo.CurrentCulture) == actualResults.Birthday, errorMessagePrefix + " " + "Birthday is not returned properly.");
            Assert.True(expectedResults.Movies.Count == actualResults.Movies.Count(), errorMessagePrefix + " " + "Movies count is not returned properly.");
            Assert.True(expectedResults.Comments.Count == actualResults.Comments.Count(), errorMessagePrefix + " " + "Comments count is not returned properly.");
            Assert.True(expectedResults.EditHistory.Count == actualResults.EditHistory.Count(), errorMessagePrefix + " " + "Comments count is not returned properly.");
        }

        [Fact]
        public void GetById_WithNonExistentHeroId_ShouldThrowInvalidOperationException()
        {
            var repo = new Mock<IRepository<Hero>>();
            repo.Setup(r => r.All()).Returns(this.GetTestData().AsQueryable);

            this._heroService = new HeroService(repo.Object, null, null, null);

            Assert.Throws<InvalidOperationException>(() => this._heroService.GetById("asd", -1));
        }

        [Fact]
        public void GetById_WithNonExistentUserId_ShouldThrowInvalidOperationException()
        {
            var repo = new Mock<IRepository<Hero>>();
            var userRepo = new Mock<IRepository<ApplicationUser>>();
            repo.Setup(r => r.All()).Returns(this.GetTestData().AsQueryable);

            this._heroService = new HeroService(repo.Object, null, userRepo.Object, null);

            Assert.Throws<InvalidOperationException>(() => this._heroService.GetById("1", 1));
        }

        [Fact]
        public void GetHeroBySearchString_WithCorrectQuery_ShouldReturnListWithHeroes()
        {
            string errorMessagePrefix = "HeroService GetHeroBySearchString() method does not work properly.";

            var repo = new Mock<IRepository<Hero>>();
            repo.Setup(r => r.All()).Returns(this.GetTestData().AsQueryable);

            this._heroService = new HeroService(repo.Object, null, null, null);

            var expected = 2;
            var actual = this._heroService.GetHeroBySearchString("Cpt").ToList().Count;

            Assert.True(expected == actual, errorMessagePrefix);
        }

        [Fact]
        public void GetHeroBySearchString_WithNonCorrectQuery_ShouldReturnEmptyList()
        {
            string errorMessagePrefix = "HeroService GetHeroBySearchString() method does not work properly.";

            var repo = new Mock<IRepository<Hero>>();
            repo.Setup(r => r.All()).Returns(this.GetTestData().AsQueryable);

            this._heroService = new HeroService(repo.Object, null, null, null);

            var expected = 0;
            var actual = this._heroService.GetHeroBySearchString("asd").ToList().Count;

            Assert.True(expected == actual, errorMessagePrefix);
        }

        [Fact]
        public void CreateHero_WithCorrectData_ShouldCreateHeroSuccessfully()
        {
            string errorMessagePrefix = "HeroService CreateHero() method does not work properly.";

            var repo = new Mock<IRepository<Hero>>();
            var fileMock = new Mock<IFormFile>();

            const string content = "Hello World from a Fake File";
            const string fileName = "profileImg.jpg";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;

            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            repo.Setup(r => r.All()).Returns(this.GetTestData().AsQueryable);

            this._heroService = new HeroService(repo.Object, new ImageService(), null, null);

            var createHeroDto = new CreateHeroDTO
            {
                Name = "11",
                RealName = "123",
                Birthday = DateTime.Now,
                Description = "123123123123123",
                Image = fileMock.Object,
                CoverImage = fileMock.Object,
                Gender = "Male",
                MovieTitle = Array.Empty<string>()
            };

            var actual = this._heroService.CreateHero(createHeroDto, true).IsCompletedSuccessfully;

            Assert.True(actual, errorMessagePrefix);
        }

        [Theory]
        [InlineData(null, "RealName", "Description", "Gender")]
        [InlineData("HeroName", null, "Description", "Gender")]
        [InlineData("HeroName", "RealName", null, "Gender")]
        [InlineData("HeroName", "RealName", "Description", null)]
        [InlineData("", "RealName", "Description", "Gender")]
        [InlineData("HeroName", "", "Description", "Gender")]
        [InlineData("HeroName", "RealName", "", "Gender")]
        [InlineData("HeroName", "RealName", "Description", "")]
        [InlineData(null, null, null, null)]
        [InlineData("", "", "", "")]
        public async Task CreateHero_WithIncorrectData_ShouldThrowArgumentException(string name, string realName, string description, string gender)
        {
            var repo = new Mock<IRepository<Hero>>();
            var fileMock = new Mock<IFormFile>();

            this._heroService = new HeroService(repo.Object, new ImageService(), null, null);

            var createHeroDto = new CreateHeroDTO
            {
                Name = name,
                RealName = realName,
                Birthday = DateTime.Now,
                Description = description,
                Image = fileMock.Object,
                CoverImage = fileMock.Object,
                Gender = gender,
                MovieTitle = Array.Empty<string>()
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => this._heroService.CreateHero(createHeroDto));
        }

        [Fact]
        public void UpdateHero_WithCorrectData_ShouldSuccessfullyUpdateHero()
        {
            string errorMessagePrefix = "HeroService UpdateHero() method does not work properly.";

            var repo = new Mock<IRepository<Hero>>();

            repo.Setup(r => r.All()).Returns(this.GetTestData().AsQueryable);

            this._heroService = new HeroService(repo.Object, null, null, null);

            var updateHeroDto = new UpdateHeroDTO()
            {
                Name = "asd"
            };

            var expectedName = "asd";
            var isPassedSuccessfully = this._heroService.UpdateHero(1, updateHeroDto).IsCompletedSuccessfully;
            var actualName = repo.Object.All().FirstOrDefault(x => x.Id == 1)?.Name;

            Assert.True(isPassedSuccessfully, errorMessagePrefix);
            Assert.True(expectedName == actualName, errorMessagePrefix);
        }

        [Fact]
        public async Task UpdateHero_WithSameNameLikePrevious_ShouldThrowArgumentException()
        {
            var repo = new Mock<IRepository<Hero>>();

            repo.Setup(r => r.All()).Returns(this.GetTestData().AsQueryable);

            this._heroService = new HeroService(repo.Object, null, null, null);

            var updateHeroDto = new UpdateHeroDTO()
            {
                Name = "Cpt America"
            };

            await Assert.ThrowsAsync<ArgumentException>(() => this._heroService.UpdateHero(1, updateHeroDto));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task UpdateHero_WithIncorrectData_ShouldThrowInvalidOperationException(string name)
        {
            var repo = new Mock<IRepository<Hero>>();

            repo.Setup(r => r.All()).Returns(this.GetTestData().AsQueryable);

            this._heroService = new HeroService(repo.Object, null, null, null);

            var updateHeroDto = new UpdateHeroDTO()
            {
                Name = name
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => this._heroService.UpdateHero(1, updateHeroDto));
        }

        [Fact]
        public void DeleteHero_WithCorrectData_ShouldSuccessfullyDeleteHero()
        {
            string errorMessagePrefix = "HeroService DeleteHero() method does not work properly.";

            var repo = new Mock<IRepository<Hero>>();
            var commentsRepo = new Mock<IRepository<Comment>>();

            repo.Setup(r => r.All()).Returns(this.GetTestData().AsQueryable);

            this._heroService = new HeroService(repo.Object, null, null, commentsRepo.Object);

            var isPassedSuccessfully = this._heroService.DeleteHero(1).IsCompletedSuccessfully;

            Assert.True(isPassedSuccessfully, errorMessagePrefix);
        }

        [Fact]
        public async Task DeleteHero_WithIncorrectHeroId_ShouldThrowInvalidOperationException()
        {
            var repo = new Mock<IRepository<Hero>>();

            repo.Setup(r => r.All()).Returns(this.GetTestData().AsQueryable);

            this._heroService = new HeroService(repo.Object, null, null, null);

            await Assert.ThrowsAsync<InvalidOperationException>(() => this._heroService.DeleteHero(-1));
        }
    }
}