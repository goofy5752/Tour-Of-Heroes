namespace TourOfHeroesServices.Tests
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Common;
    using Contracts;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.CommentDtos;
    using TourOfHeroesData.Common.Contracts;

    using Moq;
    using Xunit;

    public class CommentServiceTests
    {
        private ICommentService _commentService;

        private List<Comment> GetTestData()
        {
            return new List<Comment>()
            {
                new Comment()
                {
                    Id = 1,
                    Text = "comment 1",
                    Blog = new Blog()
                    {
                        Id = 1,
                        Comments = new List<Comment>()
                    },
                    BlogId = 1,
                    PublishedOn = DateTime.Now,
                    ProfileImage = "asd"
                },
                new Comment()
                {
                    Id = 2,
                    Text = "comment 2",
                    Blog = new Blog()
                    {
                        Id = 2,
                        Comments = new List<Comment>()
                    },
                    BlogId = 2,
                    PublishedOn = DateTime.Now,
                    ProfileImage = "asd"
                }
            };
        }

        public CommentServiceTests()
        {
            MapperInitializer.InitializeMapper();
            Thread.Sleep(100);
        }

        [Fact]
        public void GetAllComments_WithExistentData_ShouldReturnListWithAllComments()
        {
            string errorMessagePrefix = "CommentService GetAllComments() method does not work properly.";

            var commentRepo = new Mock<IRepository<Comment>>();

            commentRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable());

            this._commentService = new CommentService(commentRepo.Object, null, null ,null, null, null);

            var expectedResults = GetTestData();
            var actualResults = this._commentService.GetAllComments().ToList();

            Assert.True(expectedResults.Count == actualResults.Count(), errorMessagePrefix);

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Text == actualEntry.Text, errorMessagePrefix + " " + "Text is not returned properly.");
                Assert.True(expectedEntry.ProfileImage == actualEntry.ProfileImage, errorMessagePrefix + " " + "ProfileImage is not returned properly.");
                Assert.True(expectedEntry.UserName == actualEntry.UserName, errorMessagePrefix + " " + "UserName is not returned properly.");
                Assert.True(expectedEntry.PublishedOn.ToString(CultureInfo.CurrentCulture) == actualEntry.PublishedOn.ToString(CultureInfo.CurrentCulture), errorMessagePrefix + " " + "PublishedOn is not returned properly.");
            }
        }

        [Fact]
        public void GetAllComments_WithNonExistentData_ShouldReturnEmptyList()
        {
            string errorMessagePrefix = "CommentService GetAllComments() method does not work properly.";

            var commentRepo = new Mock<IRepository<Comment>>();

            commentRepo.Setup(x => x.All()).Returns(new List<Comment>(0).AsQueryable());

            this._commentService = new CommentService(commentRepo.Object, null, null, null, null, null);

            var expectedResults = 0;
            var actualResults = this._commentService.GetAllComments().Count();

            Assert.True(expectedResults == actualResults, errorMessagePrefix);
        }

        [Fact]
        public void CreateCommentForHero_WithCorrectData_ShouldCreateCommentSuccessfully()
        {
            string errorMessagePrefix = "CommentService CreateCommentForHero() method does not work properly.";

            var commentRepo = new Mock<IRepository<Comment>>();
            var userRepo = new Mock<IRepository<ApplicationUser>>();
            var heroRepo = new Mock<IRepository<Hero>>();
            var activityRepo = new Mock<IRepository<UserActivity>>();

            commentRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable());
            userRepo.Setup(x => x.All()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "1",
                    Activity = new List<UserActivity>()
                }
            }.AsQueryable);
            heroRepo.Setup(x => x.All()).Returns(new List<Hero>()
            {
                new Hero()
                {
                    Id = 1
                }
            }.AsQueryable);

            this._commentService = new CommentService(commentRepo.Object, heroRepo.Object, userRepo.Object, null, null, activityRepo.Object);

            var commentDto = new CreateCommentDTO()
            {
                Action = "hero",
                Comment = "Something",
                Id = 1,
                UserId = "1"
            };

            var isCompleted = this._commentService.CreateComment(commentDto, true).IsCompletedSuccessfully;

            Assert.True(isCompleted, errorMessagePrefix);
        }

        [Fact]
        public void CreateCommentForBlog_WithCorrectData_ShouldCreateCommentSuccessfully()
        {
            string errorMessagePrefix = "CommentService CreateCommentForBlog() method does not work properly.";

            var commentRepo = new Mock<IRepository<Comment>>();
            var userRepo = new Mock<IRepository<ApplicationUser>>();
            var blogRepo = new Mock<IRepository<Blog>>();
            var activityRepo = new Mock<IRepository<UserActivity>>();

            commentRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable());
            userRepo.Setup(x => x.All()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "1",
                    Activity = new List<UserActivity>()
                }
            }.AsQueryable);
            blogRepo.Setup(x => x.All()).Returns(new List<Blog>()
            {
                new Blog()
                {
                    Id = 1
                }
            }.AsQueryable);

            this._commentService = new CommentService(commentRepo.Object, null, userRepo.Object, null, blogRepo.Object, activityRepo.Object);

            var commentDto = new CreateCommentDTO()
            {
                Action = "blog",
                Comment = "Something",
                Id = 1,
                UserId = "1"
            };

            var isCompleted = this._commentService.CreateComment(commentDto, true).IsCompletedSuccessfully;

            Assert.True(isCompleted, errorMessagePrefix);
        }

        [Theory]
        [InlineData(null, "Comment", "1", 1)]
        [InlineData("Hero", null, "1", 1)]
        [InlineData("Hero", "Comment", null, 1)]
        [InlineData("Hero", "Comment", "1", -1)]
        [InlineData("", "Comment", "1", -1)]
        [InlineData("Hero", "", "1", -1)]
        [InlineData("Hero", "Comment", "", -1)]
        public async Task CreateComment_WithIncorrectData_ShouldThrowInvalidOperationException(string action, string comment, string userId, int heroId)
        {
            var commentRepo = new Mock<IRepository<Comment>>();
            var userRepo = new Mock<IRepository<ApplicationUser>>();
            var heroRepo = new Mock<IRepository<Hero>>();
            var activityRepo = new Mock<IRepository<UserActivity>>();

            commentRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable());
            userRepo.Setup(x => x.All()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "1",
                    Activity = new List<UserActivity>()
                }
            }.AsQueryable);
            heroRepo.Setup(x => x.All()).Returns(new List<Hero>()
            {
                new Hero()
                {
                    Id = 1
                }
            }.AsQueryable);

            this._commentService = new CommentService(commentRepo.Object, heroRepo.Object, userRepo.Object, null, null, activityRepo.Object);

            var commentDto = new CreateCommentDTO()
            {
                Action = action,
                Comment = comment,
                Id = heroId,
                UserId = userId
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => this._commentService.CreateComment(commentDto));
        }

        [Fact]
        public async Task CreateComment_WithIncorrectUserId_ShouldThrowInvalidOperationException()
        {
            var userRepo = new Mock<IRepository<ApplicationUser>>();

            userRepo.Setup(x => x.All()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "1"
                }
            }.AsQueryable);

            this._commentService = new CommentService(null, null, userRepo.Object, null, null, null);

            var commentDto = new CreateCommentDTO()
            {
                Action = "hero",
                Comment = "Something",
                Id = 1,
                UserId = "-1"
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => this._commentService.CreateComment(commentDto));
        }

        [Fact]
        public void DeleteComment_WithExistentCommentId_ShouldDeleteCommentProperly()
        {
            string errorMessagePrefix = "CommentService DeleteComment() method does not work properly.";

            var commentRepo = new Mock<IRepository<Comment>>();

            commentRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable());

            this._commentService = new CommentService(commentRepo.Object, null, null, null, null, null);

            var isCompleted = this._commentService.DeleteComment(1, true).IsCompletedSuccessfully;

            Assert.True(isCompleted, errorMessagePrefix);
        }

        [Fact]
        public async Task DeleteComment_WithNonExistentCommentId_ShouldThrowInvalidOperationException()
        {
            var commentRepo = new Mock<IRepository<Comment>>();

            commentRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable());

            this._commentService = new CommentService(commentRepo.Object, null, null, null, null, null);

            await Assert.ThrowsAsync<InvalidOperationException>(() => this._commentService.DeleteComment(-1, true));
        }
    }
}