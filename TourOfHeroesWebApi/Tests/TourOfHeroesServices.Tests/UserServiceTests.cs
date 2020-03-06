namespace TourOfHeroesServices.Tests
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Common;
    using Contracts;
    using TourOfHeroesDTOs.UserDtos;
    using TourOfHeroesData.Models;
    using TourOfHeroesData.Common.Contracts;

    using Moq;
    using Xunit;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class UserServiceTests
    {
        private IUserService _userService;

        private List<ApplicationUser> GetTestData()
        {
            return new List<ApplicationUser>
            {
                new ApplicationUser {
                    Id = "1",
                    UserName = "Pesho",
                    Email = "test@gmail.com",
                    FullName = "Martin Asenov",
                    JobTitle = "CTO",
                    ProfileImage = "neshto.si",
                    Blogs = new List<Blog>()
                    {
                        new Blog(),
                        new Blog()
                    },
                    Activity = new List<UserActivity>()
                    {
                        new UserActivity(),
                        new UserActivity()
                    },
                    Comments = new List<Comment>()
                    {
                        new Comment(),
                        new Comment()
                    },
                    LikedMovies = new List<LikedMovie>()
                    {
                        new LikedMovie(),
                        new LikedMovie()
                    },
                    UserBlogLikes = new List<UserBlogLikes>()
                    {
                        new UserBlogLikes(),
                        new UserBlogLikes()
                    },
                    UserBlogDislikes = new List<UserBlogDislikes>()
                    {
                        new UserBlogDislikes(),
                        new UserBlogDislikes()
                    }
                },
                new ApplicationUser {
                    Id = "2",
                    UserName = "Gosho",
                    Email = "test1@gmail.com",
                    FullName = "Gosho Goshev",
                    JobTitle = "CTO",
                    ProfileImage = "neshto.si",
                    Blogs = new List<Blog>()
                    {
                        new Blog(),
                        new Blog()
                    },
                    Activity = new List<UserActivity>()
                    {
                        new UserActivity(),
                        new UserActivity()
                    },
                    Comments = new List<Comment>()
                    {
                        new Comment(),
                        new Comment()
                    },
                    LikedMovies = new List<LikedMovie>()
                    {
                        new LikedMovie(),
                        new LikedMovie()
                    },
                    UserBlogLikes = new List<UserBlogLikes>()
                    {
                        new UserBlogLikes(),
                        new UserBlogLikes()
                    },
                    UserBlogDislikes = new List<UserBlogDislikes>()
                    {
                        new UserBlogDislikes(),
                        new UserBlogDislikes()
                    }
                },
                new ApplicationUser {
                    Id = "3",
                    UserName = "ko4inata",
                    Email = "test2@gmail.com",
                    FullName = "Koce Boce",
                    JobTitle = "CTO",
                    ProfileImage = "neshto.si",
                    Blogs = new List<Blog>()
                    {
                        new Blog(),
                        new Blog()
                    },
                    Activity = new List<UserActivity>()
                    {
                        new UserActivity(),
                        new UserActivity()
                    },
                    Comments = new List<Comment>()
                    {
                        new Comment(),
                        new Comment()
                    },
                    LikedMovies = new List<LikedMovie>()
                    {
                        new LikedMovie(),
                        new LikedMovie()
                    },
                    UserBlogLikes = new List<UserBlogLikes>()
                    {
                        new UserBlogLikes(),
                        new UserBlogLikes()
                    },
                    UserBlogDislikes = new List<UserBlogDislikes>()
                    {
                        new UserBlogDislikes(),
                        new UserBlogDislikes()
                    }
                },
            };
        }

        public UserServiceTests()
        {
            MapperInitializer.InitializeMapper();
            Thread.Sleep(100);
        }

        [Fact]
        public void GetAllUsers_WithCorrectData_ShouldSuccessfullyReturnAllUsers()
        {
            string errorMessagePrefix = "UserService GetAllUsers() method does not work properly.";

            var repo = new Mock<IRepository<ApplicationUser>>();
            repo.Setup(r => r.All()).Returns(GetTestData().AsQueryable);

            this._userService = new UserService(repo.Object, null);

            var expectedResults = GetTestData();
            var actualResults = this._userService.GetAllUsers().ToList();

            Assert.True(expectedResults.Count == actualResults.Count(), errorMessagePrefix);

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Email == actualEntry.Email, errorMessagePrefix + " " + "Email is not returned properly.");
                Assert.True(expectedEntry.FullName == actualEntry.FullName, errorMessagePrefix + " " + "FullName is not returned properly.");
                Assert.True(expectedEntry.ProfileImage == actualEntry.ProfileImage, errorMessagePrefix + " " + "ProfileImage is not returned properly.");
                Assert.True(expectedEntry.JobTitle == actualEntry.JobTitle, errorMessagePrefix + " " + "JobTitle is not returned properly.");
                Assert.True(expectedEntry.Comments.Count == actualEntry.Comments.Count(), errorMessagePrefix + " " + "Comments count is not returned properly.");
                Assert.True(expectedEntry.Blogs.Count == actualEntry.Blogs.Count(), errorMessagePrefix + " " + "Blogs count is not returned properly.");
            }
        }

        [Fact]
        public void GetAllUsers_WithEmptyData_ShouldReturnEmptyList()
        {
            string errorMessagePrefix = "UserService GetAllUsers() method does not work properly.";

            var repo = new Mock<IRepository<ApplicationUser>>();
            repo.Setup(r => r.All()).Returns(new List<ApplicationUser>().AsQueryable);

            this._userService = new UserService(repo.Object, null);

            var expectedResults = 0;
            var actualResults = this._userService.GetAllUsers().ToList();

            Assert.True(expectedResults == actualResults.Count(), errorMessagePrefix);
        }

        [Fact]
        public void GetUserDetail_WithExistentId_ShouldReturnCorrectUserDetail()
        {
            string errorMessagePrefix = "UserService GetUserDetail() method does not work properly.";

            var repo = new Mock<IRepository<ApplicationUser>>();

            repo.Setup(r => r.All()).Returns(GetTestData().AsQueryable);

            this._userService = new UserService(repo.Object, null);

            var expectedResults = GetTestData().FirstOrDefault(x => x.Id == "1");
            var actualResults = this._userService.GetUserDetail("1", true);

            Assert.True(expectedResults?.Email ==
                        actualResults.Email, errorMessagePrefix + " " + "Email is not returned properly.");
            Assert.True(expectedResults?.FullName ==
                        actualResults.FullName, errorMessagePrefix + " " + "FullName is not returned properly.");
            Assert.True(expectedResults?.ProfileImage ==
                        actualResults.ProfileImage, errorMessagePrefix + " " + "ProfileImage is not returned properly.");
            Assert.True(expectedResults?.JobTitle ==
                        actualResults.JobTitle, errorMessagePrefix + " " + "JobTitle is not returned properly.");
            Assert.True(expectedResults?.Activity.Count ==
                        actualResults.Activity.Count, errorMessagePrefix + " " + "Activity count is not returned properly.");
        }

        [Fact]
        public void GetUserDetail_WithNonExistentUserId_ShouldThrowInvalidOperationException()
        {
            var repo = new Mock<IRepository<ApplicationUser>>();

            repo.Setup(r => r.All()).Returns(GetTestData().AsQueryable);

            this._userService = new UserService(repo.Object, null);

            Assert.Throws<InvalidOperationException>(() => this._userService.GetUserDetail("-1"));
        }

        [Fact]
        public void UpdateUser_WithCorrectData_ShouldUpdateUserProperly()
        {
            string errorMessagePrefix = "UserService UpdateUser() method does not work properly.";

            var repo = new Mock<IRepository<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<ApplicationUser>>().Object,
                new IUserValidator<ApplicationUser>[0],
                new IPasswordValidator<ApplicationUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<ApplicationUser>>>().Object);

            repo.Setup(r => r.All()).Returns(GetTestData().AsQueryable);

            this._userService = new UserService(repo.Object, mockUserManager.Object);

            var updateUserDto = new UpdateUserDTO
            {
                Role = "Editor"
            };

            var isCompleted = this._userService.UpdateUser("1", updateUserDto, true).IsCompletedSuccessfully;

            Assert.True(isCompleted, errorMessagePrefix);
        }

        [Fact]
        public async Task UpdateUser_WithIncorrectUserId_ShouldThrowInvalidOperationException()
        {
            var repo = new Mock<IRepository<ApplicationUser>>();

            repo.Setup(r => r.All()).Returns(GetTestData().AsQueryable);

            this._userService = new UserService(repo.Object, null);

            await Assert.ThrowsAsync<InvalidOperationException>(() => this._userService.UpdateUser("-1", null));
        }

        [Fact]
        public async Task UpdateUser_WithIncorrectUserRole_ShouldThrowInvalidOperationException()
        {
            var repo = new Mock<IRepository<ApplicationUser>>();

            repo.Setup(r => r.All()).Returns(GetTestData().AsQueryable);

            this._userService = new UserService(repo.Object, null);

            var updateUserDto = new UpdateUserDTO()
            {
                Role = "go6o"
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => this._userService.UpdateUser("1", updateUserDto));
        }

        [Fact]
        public void DeleteUser_WithCorrectUserId_ShouldDeleteUserSuccessfully()
        {
            string errorMessagePrefix = "UserService DeleteUser() method does not work properly.";

            var repo = new Mock<IRepository<ApplicationUser>>();

            repo.Setup(r => r.All()).Returns(GetTestData().AsQueryable);

            this._userService = new UserService(repo.Object, null);

            var isCompleted = this._userService.DeleteUser("1").IsCompletedSuccessfully;

            Assert.True(isCompleted, errorMessagePrefix);
        }

        [Fact]
        public async Task DeleteUser_WithIncorrectUserId_ShouldThrowInvalidOperationException()
        {
            var repo = new Mock<IRepository<ApplicationUser>>();

            repo.Setup(r => r.All()).Returns(GetTestData().AsQueryable);

            this._userService = new UserService(repo.Object, null);

            await Assert.ThrowsAsync<InvalidOperationException>(() => this._userService.DeleteUser("-1"));
        }
    }
}