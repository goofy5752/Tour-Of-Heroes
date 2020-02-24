namespace TourOfHeroesServices.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using TourOfHeroesData;
    using TourOfHeroesData.Common.Contracts;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.ProfileDtos;

    using Common;
    using Xunit;
    using Moq;

    using Microsoft.AspNetCore.Http;

    public class ProfileServiceTests
    {
        private IProfileService _profileService;

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
                        new Blog()
                        {
                            UserId = "1"
                        },
                        new Blog()
                        {
                            UserId = "1"
                        }
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
                        new UserBlogLikes()
                        {
                            UserId = "1"
                        },
                        new UserBlogLikes()
                        {
                            UserId = "1"
                        }
                    },
                    UserBlogDislikes = new List<UserBlogDislikes>()
                    {
                        new UserBlogDislikes()
                        {
                            UserId = "1"
                        },
                        new UserBlogDislikes()
                        {
                            UserId = "1"
                        }
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
                    }
                },
            };
        }

        private async Task SeedData(TourOfHeroesDbContext context)
        {
            context.AddRange(GetTestData());
            await context.SaveChangesAsync();
        }

        public ProfileServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public void GetProfile_WithExistentId_ShouldReturnCorrectProfileDetails()
        {
            string errorMessagePrefix = "ProfileService GetProfile() method does not work properly.";

            var userRepo = new Mock<IRepository<ApplicationUser>>();
            var userBlogLikes = new Mock<IRepository<UserBlogLikes>>();
            var userBlogDislikes = new Mock<IRepository<UserBlogDislikes>>();

            userRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);
            userBlogLikes.Setup(x => x.All()).Returns(new List<UserBlogLikes>()
            {
                new UserBlogLikes()
                {
                    UserId = "1"
                },
                new UserBlogLikes()
                {
                    UserId = "1"
                }
            }.AsQueryable());
            userBlogDislikes.Setup(x => x.All()).Returns(new List<UserBlogDislikes>()
            {
                new UserBlogDislikes()
                {
                    UserId = "1"
                },
                new UserBlogDislikes()
                {
                    UserId = "1"
                }
            }.AsQueryable());

            this._profileService = new ProfileService(userRepo.Object, null, null, userBlogLikes.Object, userBlogDislikes.Object, null);

            var expectedResults = this.GetTestData().FirstOrDefault(x => x.Id == "1");
            var actualResults = this._profileService.GetProfile("1");

            Assert.True(expectedResults.Email == actualResults.Email, errorMessagePrefix + " " + "Email is not returned properly.");
            Assert.True(expectedResults.FullName == actualResults.FullName, errorMessagePrefix + " " + "FullName is not returned properly.");
            Assert.True(expectedResults.ProfileImage == actualResults.ProfileImage, errorMessagePrefix + " " + "ProfileImage is not returned properly.");
            Assert.True(expectedResults.JobTitle == actualResults.JobTitle, errorMessagePrefix + " " + "JobTitle is not returned properly.");
            Assert.True(expectedResults.Comments.Count == actualResults.Comments.Count(), errorMessagePrefix + " " + "Comments count is not returned properly.");
            Assert.True(expectedResults.Blogs.Count == actualResults.Blogs.Count(), errorMessagePrefix + " " + "Blogs count is not returned properly.");
            Assert.True(expectedResults.UserBlogLikes.Count == actualResults.PostLikes, errorMessagePrefix + " " + "Blogs count is not returned properly.");
            Assert.True(expectedResults.UserBlogDislikes.Count == actualResults.PostDislikes, errorMessagePrefix + " " + "Blogs count is not returned properly.");
        }

        [Fact]
        public void GetProfile_WithNonExistentId_ShouldThrowInvalidOperationException()
        {
            var userRepo = new Mock<IRepository<ApplicationUser>>();
            var userBlogLikes = new Mock<IRepository<UserBlogLikes>>();
            var userBlogDislikes = new Mock<IRepository<UserBlogDislikes>>();

            userRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);

            this._profileService = new ProfileService(userRepo.Object, null, null, userBlogLikes.Object, userBlogDislikes.Object, null);

            Assert.Throws<ArgumentException>(() => this._profileService.GetProfile("4"));
        }

        [Fact]
        public void UpdateProfileImage_WithCorrectFile_ShouldUpdateProfileImageProperly()
        {
            string errorMessagePrefix = "ProfileService UpdateProfileImage() method does not work properly.";

            var userRepo = new Mock<IRepository<ApplicationUser>>();
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
            userRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);

            this._profileService = new ProfileService(userRepo.Object, new ImageService(), null, null, null, null);

            var isPassed = this._profileService.UpdateProfileImage("1", new UpdateProfileImageDTO()
            {
                ProfileImage = fileMock.Object,
                UserId = "1"
            }).IsCompleted;

            Assert.True(isPassed, errorMessagePrefix);
        }

        [Fact]
        public async Task UpdateProfileImage_WithIncorrectFile_ShouldThrowAnException()
        {
            var userRepo = new Mock<IRepository<ApplicationUser>>();
            var fileMock = new Mock<IFormFile>();

            userRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);

            this._profileService = new ProfileService(userRepo.Object, new ImageService(), null, null, null, null);

            await Assert.ThrowsAsync<ArgumentException>(() => this._profileService.UpdateProfileImage("1", new UpdateProfileImageDTO()
            {
                ProfileImage = fileMock.Object,
                UserId = "1"
            }));
        }

        [Fact]
        public async Task UpdateProfileImage_WithIncorrectUserId_ShouldThrowAnException()
        {
            var userRepo = new Mock<IRepository<ApplicationUser>>();
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
            userRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);

            userRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);

            this._profileService = new ProfileService(userRepo.Object, new ImageService(), null, null, null, null);

            await Assert.ThrowsAsync<ArgumentException>(() => this._profileService.UpdateProfileImage("1", new UpdateProfileImageDTO()
            {
                ProfileImage = fileMock.Object,
                UserId = "1"
            }));
        }

        [Fact]
        public void UpdateProfileEmail_WithCorrectData_ShouldWorkProperly()
        {
            string errorMessagePrefix = "ProfileService UpdateProfileEmail() method does not work properly.";
            var userRepo = new Mock<IRepository<ApplicationUser>>();
            var userActivityRepo = new Mock<IRepository<UserActivity>>();

            userRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);

            this._profileService = new ProfileService(userRepo.Object, null, null, null, null, userActivityRepo.Object);

            this._profileService.UpdateProfileEmail("1", new UpdateProfileEmailDTO()
            {
                Email = "asdasd@abv.bg"
            });

            var expected = "asdasd@abv.bg";
            var actual = userRepo.Object.All().FirstOrDefault(x => x.Id == "1")?.Email;

            Assert.True(expected == actual, errorMessagePrefix);
        }

        [Theory]
        [InlineData("1", "")]
        [InlineData("1", "asd")]
        [InlineData("1", "-1")]
        [InlineData("1", "123@abc")]
        [InlineData("1", "@abv.bg")]
        [InlineData("1", "asd@abv.")]
        public async Task UpdateProfileEmail_WithIncorrectEmail_ShouldThrowAnArgumentException(string id, string email)
        {
            var userRepo = new Mock<IRepository<ApplicationUser>>();

            userRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);

            this._profileService = new ProfileService(userRepo.Object, null, null, null, null, null);

            await Assert.ThrowsAsync<ArgumentException>(() => this._profileService.UpdateProfileEmail(id, new UpdateProfileEmailDTO()
            {
                Email = email
            }));
        }

        [Fact]
        public async Task UpdateProfileEmail_WithIncorrectUserId_ShouldThrowAnArgumentException()
        {
            var userRepo = new Mock<IRepository<ApplicationUser>>();

            userRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);

            this._profileService = new ProfileService(userRepo.Object, null, null, null, null, null);

            await Assert.ThrowsAsync<ArgumentException>(() => this._profileService.UpdateProfileEmail("-1", new UpdateProfileEmailDTO()
            {
                Email = "asdsd@abv.bg"
            }));
        }
    }
}