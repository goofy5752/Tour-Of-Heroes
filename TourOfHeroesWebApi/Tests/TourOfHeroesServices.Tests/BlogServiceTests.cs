using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Moq;
using TourOfHeroesData.Common.Contracts;
using TourOfHeroesData.Models;
using TourOfHeroesServices.Contracts;
using TourOfHeroesServices.Tests.Common;
using Xunit;

namespace TourOfHeroesServices.Tests
{
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
    }
}