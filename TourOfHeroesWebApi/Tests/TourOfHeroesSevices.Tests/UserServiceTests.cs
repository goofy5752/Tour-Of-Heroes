namespace TourOfHeroesServices.Tests
{
    using Moq;
    using Xunit;
    using Contracts;
    using System.Linq;
    using System.Collections.Generic;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.UserDtos;
    using TourOfHeroesData.Common.Contracts;

    public class UserServiceTests
    {
        public List<ApplicationUser> GetTestData()
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
            };
        }

        [Fact]
        public void RepositoryShouldCallAllMethodOnce()
        {
            var repo = new Mock<IRepository<ApplicationUser>>();

            repo.Setup(r => r.All())
                .Returns(GetTestData().AsQueryable());

            IUserService service = new UserService(repo.Object, null);

            service.UpdateUser("1", new UpdateUserDTO()
            {
                Role = "Admin"
            });

            repo.Verify(x => x.All(), Times.Once);
        }
    }
}