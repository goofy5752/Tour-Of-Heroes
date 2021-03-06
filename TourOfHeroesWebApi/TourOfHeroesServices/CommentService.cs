﻿using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TourOfHeroesServices
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TourOfHeroesData.Common.Contracts;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.CommentDtos;

    using RealTimeHub;
    using Contracts;

    using Microsoft.AspNetCore.SignalR;

    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<Hero> _heroRepository;
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IHubContext<CommentHub, ITypedHubClient> _hubContext;
        private readonly IRepository<Blog> _blogRepository;
        private readonly IRepository<UserActivity> _userActivityRepository;

        public CommentService(IRepository<Comment> commentRepository, IRepository<Hero> heroRepository, IRepository<ApplicationUser> userRepository, IHubContext<CommentHub, ITypedHubClient> hubContext, IRepository<Blog> blogRepository, IRepository<UserActivity> userActivityRepository)
        {
            _commentRepository = commentRepository;
            _heroRepository = heroRepository;
            _userRepository = userRepository;
            _hubContext = hubContext;
            _blogRepository = blogRepository;
            _userActivityRepository = userActivityRepository;
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return this._commentRepository.All().ToList();
        }

        public async Task CreateComment(CreateCommentDTO commentDto, bool skipMethodForTest = false)
        {
            var userObj = this._userRepository.All().Single(x => x.Id == commentDto.UserId);

            if (string.IsNullOrEmpty(commentDto.Action) || string.IsNullOrEmpty(commentDto.Comment) || string.IsNullOrEmpty(commentDto.UserId))
            {
                throw new InvalidOperationException("Invalid data.");
            }

            if (commentDto.Action.ToLower() == "hero")
            {
                var heroObj = this._heroRepository.All().Single(x => x.Id == commentDto.Id);

                var commentObj = new Comment
                {
                    Text = commentDto.Comment,
                    UserName = userObj.UserName,
                    ProfileImage = userObj.ProfileImage,
                    HeroId = heroObj.Id,
                    UserId = userObj.Id
                };

                var activity = new UserActivity
                {
                    Action = $"Added comment to hero '{heroObj.Name}'",
                    RegisteredOn = DateTime.Now,
                    UserId = userObj.Id,
                    User = userObj
                };

                if (!skipMethodForTest)
                {
                    await _hubContext.Clients.All.BroadcastComment(commentObj);
                }

                heroObj.Comments.Add(commentObj);
                userObj.Comments.Add(commentObj);
                await this._userActivityRepository.AddAsync(activity);
                userObj.Activity.Add(activity);

                await this._heroRepository.SaveChangesAsync();
            }
            else
            {
                var blogObj = this._blogRepository.All().Single(x => x.Id == commentDto.Id);

                var commentObj = new Comment
                {
                    Text = commentDto.Comment,
                    UserName = userObj.UserName,
                    ProfileImage = userObj.ProfileImage,
                    BlogId = blogObj.Id,
                    UserId = userObj.Id
                };

                var activity = new UserActivity
                {
                    Action = $"Added comment to post with title '{blogObj.Title}'",
                    RegisteredOn = DateTime.Now,
                    UserId = userObj.Id,
                    User = userObj
                };

                if (!skipMethodForTest)
                {
                    await _hubContext.Clients.All.BroadcastComment(commentObj);
                }

                blogObj.Comments.Add(commentObj);
                userObj.Comments.Add(commentObj);
                await this._userActivityRepository.AddAsync(activity);
                userObj.Activity.Add(activity);

                await this._blogRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteComment(int id, bool skipMethodForTest = false)
        {
            var commentToDelete = this._commentRepository.All().Single(x => x.Id == id);

            commentToDelete.IsDeleted = true;
            commentToDelete.DeletedOn = DateTime.Now;

            if (!skipMethodForTest)
            {
                await _hubContext.Clients.All.DeleteComment(id);
            }

            await this._commentRepository.SaveChangesAsync();
        }
    }
}