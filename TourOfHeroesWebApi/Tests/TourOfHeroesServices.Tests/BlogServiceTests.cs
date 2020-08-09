namespace TourOfHeroesServices.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Common;
    using Contracts;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.BlogDtos;
    using TourOfHeroesData.Common.Contracts;

    using Moq;
    using Xunit;

    using Microsoft.AspNetCore.Http;

    public class BlogServiceTests
    {
        private IBlogService _blogService;

        private List<Blog> GetTestData()
        {
            return new List<Blog>()
            {
                new Blog()
                {
                    Id = 1,
                    AuthorUserName = "Admin",
                    BlogImage = "image.com",
                    Content = "Something something",
                    Title = "Title",
                    PublishedOn = DateTime.Now,
                    User = new ApplicationUser()
                    {
                        Id = "1"
                    },
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            BlogId = 1
                        },
                        new Comment()
                        {
                            BlogId = 1
                        }
                    },
                    BlogUserLikes = new List<UserBlogLikes>()
                    {
                        new UserBlogLikes()
                        {
                            BlogId = 1
                        },
                        new UserBlogLikes()
                        {
                            BlogId = 1
                        }
                    },
                    BlogUserDislikes = new List<UserBlogDislikes>()
                    {
                        new UserBlogDislikes()
                        {
                            BlogId = 1
                        },
                        new UserBlogDislikes()
                        {
                            BlogId = 1
                        }
                    }
                },
                new Blog()
                {
                    Id = 2,
                    AuthorUserName = "Admin2",
                    BlogImage = "image.com2",
                    Content = "Something something2",
                    Title = "Title2",
                    PublishedOn = DateTime.Now,
                    User = new ApplicationUser()
                    {
                        Id = "2"
                    },
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            BlogId = 2
                        },
                        new Comment()
                        {
                            BlogId = 2
                        }
                    },
                    BlogUserLikes = new List<UserBlogLikes>()
                    {
                        new UserBlogLikes()
                        {
                            BlogId = 2
                        },
                        new UserBlogLikes()
                        {
                            BlogId = 2
                        }
                    },
                    BlogUserDislikes = new List<UserBlogDislikes>()
                    {
                        new UserBlogDislikes()
                        {
                            BlogId = 2
                        },
                        new UserBlogDislikes()
                        {
                            BlogId = 2
                        }
                    }
                }
            };
        }

        public BlogServiceTests()
        {
            MapperInitializer.InitializeMapper();
            Thread.Sleep(100);
        }

        [Fact]
        public void GetAllPosts_WithExistentData_ShouldReturnAllBlogPosts()
        {
            string errorMessagePrefix = "BlogService GetAllPosts() method does not work properly.";

            var blogRepo = new Mock<IRepository<Blog>>();

            blogRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable());

            this._blogService = new BlogService(blogRepo.Object, null, null, null, null, null, null);

            var expectedResults = GetTestData();
            var actualResults = this._blogService.GetAllPosts().ToList();

            Assert.True(expectedResults.Count == actualResults.Count(), errorMessagePrefix);

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.AuthorUserName == actualEntry.AuthorUserName, errorMessagePrefix + " " + "AuthorUserName is not returned properly.");
                Assert.True(expectedEntry.Title == actualEntry.Title, errorMessagePrefix + " " + "Title is not returned properly.");
                Assert.True(expectedEntry.BlogImage == actualEntry.BlogImage, errorMessagePrefix + " " + "BlogImage is not returned properly.");
                Assert.True(expectedEntry.Content == actualEntry.Content, errorMessagePrefix + " " + "Content is not returned properly.");
                Assert.True(expectedEntry.PublishedOn.ToString(CultureInfo.CurrentCulture) == actualEntry.PublishedOn.ToString(CultureInfo.CurrentCulture), errorMessagePrefix + " " + "PublishedOn is not returned properly.");
            }
        }

        [Fact]
        public void GetAllPosts_WithNonExistentData_ShouldReturnEmptyList()
        {
            string errorMessagePrefix = "BlogService GetAllPosts() method does not work properly.";

            var blogRepo = new Mock<IRepository<Blog>>();

            blogRepo.Setup(x => x.All()).Returns(new List<Blog>().AsQueryable);

            this._blogService = new BlogService(blogRepo.Object, null, null, null, null, null, null);

            var expectedResults = 0;
            var actualResults = this._blogService.GetAllPosts().Count();

            Assert.True(expectedResults == actualResults, errorMessagePrefix);
        }

        [Fact]
        public void GetPostDetail_WithExistentBlogId_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "BlogService GetPostDetail() method does not work properly.";

            var blogRepo = new Mock<IRepository<Blog>>();
            var userRepo = new Mock<IRepository<ApplicationUser>>();
            var likeRepo = new Mock<IRepository<UserBlogLikes>>();
            var dislikeRepo = new Mock<IRepository<UserBlogDislikes>>();

            blogRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);
            likeRepo.Setup(x => x.All()).Returns(new List<UserBlogLikes>()
            {
                new UserBlogLikes()
                {
                    BlogId = 1
                },
                new UserBlogLikes()
                {
                    BlogId = 1
                }
            }.AsQueryable);
            dislikeRepo.Setup(x => x.All()).Returns(new List<UserBlogDislikes>()
            {
                new UserBlogDislikes()
                {
                    BlogId = 1
                },
                new UserBlogDislikes()
                {
                    BlogId = 1
                }
            }.AsQueryable);
            userRepo.Setup(x => x.All()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "1"
                }
            }.AsQueryable);


            this._blogService = new BlogService(blogRepo.Object, userRepo.Object, null, likeRepo.Object,
                dislikeRepo.Object, null, null);

            var expectedResults = this.GetTestData().FirstOrDefault(x => x.Id == 1);
            var actualResults = this._blogService.GetPostDetail("1", 1);

            Assert.True(expectedResults.AuthorUserName == actualResults.AuthorUserName, errorMessagePrefix + " " + "AuthorUserName is not returned properly.");
            Assert.True(expectedResults.Title == actualResults.Title, errorMessagePrefix + " " + "Title is not returned properly.");
            Assert.True(expectedResults.BlogImage == actualResults.BlogImage, errorMessagePrefix + " " + "BlogImage is not returned properly.");
            Assert.True(expectedResults.Content == actualResults.Content, errorMessagePrefix + " " + "Content is not returned properly.");
            Assert.True(expectedResults.PublishedOn.ToString(CultureInfo.CurrentCulture) == actualResults.PublishedOn.ToString(CultureInfo.CurrentCulture), errorMessagePrefix + " " + "PublishedOn is not returned properly.");
            Assert.True(expectedResults.BlogUserLikes.Count(x => x.BlogId == 1) == actualResults.Likes, errorMessagePrefix + " " + "Likes is not returned properly.");
            Assert.True(expectedResults.BlogUserDislikes.Count(x => x.BlogId == 1) == actualResults.Dislikes, errorMessagePrefix + " " + "Dislikes is not returned properly.");
            Assert.True(expectedResults.Comments.Count == actualResults.Comments.Count(), errorMessagePrefix + " " + "Comments is not returned properly.");
        }

        [Fact]
        public void GetPostDetail_WithNonExistentBlogId_ShouldThrowInvalidOperationException()
        {
            var blogRepo = new Mock<IRepository<Blog>>();

            blogRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);

            this._blogService = new BlogService(blogRepo.Object, null, null, null,
                null, null, null);

            Assert.Throws<InvalidOperationException>(() => this._blogService.GetPostDetail("asd", -1));
        }

        [Fact]
        public void GetPostDetail_WithNonExistentUserId_ShouldThrowInvalidOperationException()
        {
            var blogRepo = new Mock<IRepository<Blog>>();
            var userRepo = new Mock<IRepository<ApplicationUser>>();

            blogRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);
            userRepo.Setup(x => x.All()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "1"
                }
            }.AsQueryable);

            this._blogService = new BlogService(blogRepo.Object, userRepo.Object, null, null,
                null, null, null);

            Assert.Throws<InvalidOperationException>(() => this._blogService.GetPostDetail("-1", 1));
        }

        [Fact]
        public void LikePost_WithExistentUserAndBlogIds_ShouldLikePostSuccessfully()
        {
            string errorMessagePrefix = "BlogService LikePost() method does not work properly.";

            var blogRepo = new Mock<IRepository<Blog>>();
            var userRepo = new Mock<IRepository<ApplicationUser>>();
            var activityRepo = new Mock<IRepository<UserActivity>>();
            var dislikeRepo = new Mock<IRepository<UserBlogDislikes>>();
            var likeRepo = new Mock<IRepository<UserBlogLikes>>();

            blogRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);
            userRepo.Setup(x => x.All()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "1",
                    Activity = new List<UserActivity>()
                }
            }.AsQueryable);
            
            this._blogService = new BlogService(blogRepo.Object, userRepo.Object, null, likeRepo.Object,
                dislikeRepo.Object, activityRepo.Object, null);

            var isPassed = this._blogService.LikePost("1", 1).IsCompletedSuccessfully;

            Assert.True(isPassed, errorMessagePrefix);
        }

        [Theory]
        [InlineData("-1", 2)]
        [InlineData("1", -2)]
        [InlineData("-1", -1)]
        public async Task LikePost_WithNonExistentUserAndBlogId_ShouldThrowInvalidOperationException(string userId, int blogId)
        {
            var blogRepo = new Mock<IRepository<Blog>>();
            var userRepo = new Mock<IRepository<ApplicationUser>>();

            blogRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);
            userRepo.Setup(x => x.All()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "1"
                }
            }.AsQueryable);

            this._blogService = new BlogService(blogRepo.Object, userRepo.Object, null, null,
                null, null, null);

            await Assert.ThrowsAsync<InvalidOperationException>(() => this._blogService.LikePost(userId, blogId));
        }

        [Fact]
        public async Task LikePost_WithUserIdWhoIsAlreadyLikedThisPost_ShouldThrowException()
        {
            var blogRepo = new Mock<IRepository<Blog>>();
            var userRepo = new Mock<IRepository<ApplicationUser>>();
            var likesRepo = new Mock<IRepository<UserBlogLikes>>();

            blogRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);
            likesRepo.Setup(x => x.All()).Returns(new List<UserBlogLikes>()
            {
                new UserBlogLikes()
                {
                    BlogId = 1,
                    UserId = "1"
                }
            }.AsQueryable);
            userRepo.Setup(x => x.All()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "1"
                }
            }.AsQueryable);

            this._blogService = new BlogService(blogRepo.Object, userRepo.Object, null, likesRepo.Object,
                null, null, null);

            await Assert.ThrowsAsync<Exception>(() => this._blogService.LikePost("1", 1));
        }

        [Fact]
        public void DislikePost_WithExistentUserAndBlogIds_ShouldDislikePostSuccessfully()
        {
            string errorMessagePrefix = "BlogService DislikePost() method does not work properly.";

            var blogRepo = new Mock<IRepository<Blog>>();
            var userRepo = new Mock<IRepository<ApplicationUser>>();
            var activityRepo = new Mock<IRepository<UserActivity>>();
            var dislikeRepo = new Mock<IRepository<UserBlogDislikes>>();
            var likeRepo = new Mock<IRepository<UserBlogLikes>>();

            blogRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);
            userRepo.Setup(x => x.All()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "1",
                    Activity = new List<UserActivity>()
                }
            }.AsQueryable);

            this._blogService = new BlogService(blogRepo.Object, userRepo.Object, null, likeRepo.Object,
                dislikeRepo.Object, activityRepo.Object, null);

            var isPassed = this._blogService.LikePost("1", 1).IsCompletedSuccessfully;

            Assert.True(isPassed, errorMessagePrefix);
        }

        [Theory]
        [InlineData("-1", 2)]
        [InlineData("1", -2)]
        [InlineData("-1", -1)]
        public async Task DislikePost_WithNonExistentUserAndBlogId_ShouldThrowInvalidOperationException(string userId, int blogId)
        {
            var blogRepo = new Mock<IRepository<Blog>>();
            var userRepo = new Mock<IRepository<ApplicationUser>>();

            blogRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);
            userRepo.Setup(x => x.All()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "1"
                }
            }.AsQueryable);

            this._blogService = new BlogService(blogRepo.Object, userRepo.Object, null, null,
                null, null, null);

            await Assert.ThrowsAsync<InvalidOperationException>(() => this._blogService.DislikePost(userId, blogId));
        }

        [Fact]
        public async Task DislikePost_WithUserIdWhoIsAlreadyDislikedThisPost_ShouldThrowException()
        {
            var blogRepo = new Mock<IRepository<Blog>>();
            var userRepo = new Mock<IRepository<ApplicationUser>>();
            var likeRepo = new Mock<IRepository<UserBlogLikes>>();

            blogRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);
            likeRepo.Setup(x => x.All()).Returns(new List<UserBlogLikes>()
            {
                new UserBlogLikes()
                {
                    BlogId = 1,
                    UserId = "1"
                }
            }.AsQueryable);
            userRepo.Setup(x => x.All()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "1"
                }
            }.AsQueryable);

            this._blogService = new BlogService(blogRepo.Object, userRepo.Object, null, likeRepo.Object,
                null, null, null);

            await Assert.ThrowsAsync<Exception>(() => this._blogService.LikePost("1", 1));
        }

        [Fact]
        public void CreatePost_WithCorrectData_ShouldCreatePostSuccessfully()
        {
            string errorMessagePrefix = "BlogService CreatePost() method does not work properly.";

            var blogRepo = new Mock<IRepository<Blog>>();
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
            blogRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);
            userRepo.Setup(x => x.All()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "1"
                }
            }.AsQueryable);

            this._blogService = new BlogService(blogRepo.Object, userRepo.Object, null, null,
                null, null, null);

            var postDto = new CreateBlogPostDTO()
            {
                Title = "George",
                Content = "qwerty qwerty qwerty",
                BlogImage = fileMock.Object
            };

            var passed = this._blogService.CreatePost(postDto, "1", true).IsCompletedSuccessfully;

            Assert.True(passed, errorMessagePrefix);
        }

        [Theory]
        [InlineData(null, "asd")]
        [InlineData("asd", null)]
        [InlineData("", "asd")]
        [InlineData("asd", "")]
        [InlineData(null, null)]
        [InlineData("", "")]
        public async Task CreatePost_WithIncorrectData_ShouldThrowInvalidOperationException(string title, string content)
        {
            var blogRepo = new Mock<IRepository<Blog>>();
            var userRepo = new Mock<IRepository<ApplicationUser>>();
            var fileMock = new Mock<IFormFile>();

            blogRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);
            userRepo.Setup(x => x.All()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "1"
                }
            }.AsQueryable);

            this._blogService = new BlogService(blogRepo.Object, userRepo.Object, null, null,
                null, null, null);

            var postDto = new CreateBlogPostDTO()
            {
                Title = title,
                Content = content,
                BlogImage = fileMock.Object
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                this._blogService.CreatePost(postDto, "1", true));
        }

        [Fact]
        public async Task CreatePost_WithIncorrectFile_ShouldThrowInvalidOperationException()
        {
            var blogRepo = new Mock<IRepository<Blog>>();
            var userRepo = new Mock<IRepository<ApplicationUser>>();

            blogRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);
            userRepo.Setup(x => x.All()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "1"
                }
            }.AsQueryable);

            this._blogService = new BlogService(blogRepo.Object, userRepo.Object, null, null,
                null, null, null);

            var postDto = new CreateBlogPostDTO()
            {
                Title = "asdasd",
                Content = "asdads",
                BlogImage = null
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                this._blogService.CreatePost(postDto, "1", true));
        }

        [Fact]
        public async Task CreatePost_WithIncorrectUserId_ShouldThrowInvalidOperationException()
        {
            var userRepo = new Mock<IRepository<ApplicationUser>>();

            userRepo.Setup(x => x.All()).Returns(new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = "1"
                }
            }.AsQueryable);

            this._blogService = new BlogService(null, userRepo.Object, null, null,
                null, null, null);

            var postDto = new CreateBlogPostDTO();

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                this._blogService.CreatePost(postDto, "1", true));
        }

        [Fact]
        public void EditPost_WithCorrectData_ShouldEditPostSuccessfully()
        {
            string errorMessagePrefix = "BlogService EditPost() method does not work properly.";

            var blogRepo = new Mock<IRepository<Blog>>();
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
            blogRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);

            this._blogService = new BlogService(blogRepo.Object, null, null, null,
                null, null, null);

            var expected = new EditBlogPostDTO
            {
                Id = 1,
                Title = "George",
                Content = "qwerty qwerty qwerty",
                BlogImage = fileMock.Object
            };

            var actual = blogRepo.Object.All().Single(x => x.Id == 1);

            var passed = this._blogService.EditPost(expected, true).IsCompletedSuccessfully;

            Assert.True(passed, errorMessagePrefix);

            Assert.True(expected.Content == actual.Content, errorMessagePrefix + " " + "Content is not returned properly.");
            Assert.True(expected.Title == actual.Title, errorMessagePrefix + " " + "Title is not returned properly.");
        }

        [Fact]
        public void DeletePost_WithExistentId_ShouldDeletePostProperly()
        {
            string errorMessagePrefix = "BlogService DeletePost() method does not work properly.";

            var blogRepo = new Mock<IRepository<Blog>>();
            var commentRepo = new Mock<IRepository<Comment>>();

            blogRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);
            commentRepo.Setup(x => x.All()).Returns(new List<Comment>()
            {
                new Comment()
                {
                    BlogId = 1
                },
                new Comment()
                {
                    BlogId = 1
                }
            }.AsQueryable);

            this._blogService = new BlogService(blogRepo.Object, null, null, null,
                null, null, commentRepo.Object);

            var isPassed = this._blogService.DeletePost(1).IsCompletedSuccessfully;

            Assert.True(isPassed, errorMessagePrefix);
        }

        [Fact]
        public async Task DeletePost_WithNonExistentId_ShouldThrowInvalidOperationException()
        {
            var blogRepo = new Mock<IRepository<Blog>>();

            blogRepo.Setup(x => x.All()).Returns(this.GetTestData().AsQueryable);

            this._blogService = new BlogService(blogRepo.Object, null, null, null,
                null, null, null);

            await Assert.ThrowsAsync<InvalidOperationException>(() => this._blogService.DeletePost(-1));
        }
    }
}