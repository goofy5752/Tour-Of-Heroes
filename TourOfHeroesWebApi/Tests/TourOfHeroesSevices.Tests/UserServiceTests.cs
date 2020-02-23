using System.Threading.Tasks;
using TourOfHeroesData;
using TourOfHeroesMapping.Mapping;

namespace TourOfHeroesServices.Tests
{
    using System.Linq;
    using System.Collections.Generic;

    using Common;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.UserDtos;
    using TourOfHeroesData.Common.Contracts;

    using Moq;
    using Xunit;
    using Contracts;

    public class UserServiceTests
    {
        private List<ApplicationUser> GetTestData()
        {
            return new List<ApplicationUser>
            {
                new ApplicationUser {
                    Id = "1",
                    UserName = "Pesho",
                    Email = "test@gmail.com",
                    FullName = "Martin Asenov"
                },
                new ApplicationUser {
                    Id = "2",
                    UserName = "Gosho",
                    Email = "test1@gmail.com",
                    FullName = "Gosho Goshev"
                },
                new ApplicationUser {
                    Id = "3",
                    UserName = "ko4inata",
                    Email = "test2@gmail.com",
                    FullName = "Koce Boce"
                },
            };
        }

        private async Task SeedData(TourOfHeroesDbContext context)
        {
            context.AddRange(GetTestData());
            await context.SaveChangesAsync();
        }

        public UserServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public void GetAllUsers_WithCorrectData_ShouldSuccessfullyGetUsers()
        {
            string errorMessagePrefix = "UserService GetAllUsers() method does not work properly.";

            //var context = TourOfHeroesDbContextInMemoryFactory.InitializeContext();
            //await SeedData(context);
            var repo = new Mock<IRepository<ApplicationUser>>();
            repo.Setup(r => r.All()).Returns(GetTestData().AsQueryable);

            IUserService service = new UserService(repo.Object, null);

            var expectedResults = GetTestData();
            var actualResults = service.GetAllUsers().ToList();

            Assert.True(expectedResults.Count == actualResults.Count(), errorMessagePrefix);

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Email == actualEntry.Email, errorMessagePrefix + " " + "Price is not returned properly.");
                Assert.True(expectedEntry.FullName == actualEntry.FullName, errorMessagePrefix + " " + "Picture is not returned properly.");
            }
        }
    }
}