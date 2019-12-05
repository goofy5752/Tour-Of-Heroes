namespace TourOfHeroesServices
{
    using Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TourOfHeroesData.Common.Contracts;
    using TourOfHeroesData.Models;
    using Microsoft.AspNetCore.SignalR;
    using TourOfHeroesDTOs.CommentDtos;
    using RealTimeHub;

    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<Hero> _heroRepository;
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IHubContext<CommentHub, ITypedHubClient> _hubContext;
        private readonly IRepository<Blog> _blogRepository;

        public CommentService(IRepository<Comment> commentRepository, IRepository<Hero> heroRepository, IRepository<ApplicationUser> userRepository, IHubContext<CommentHub, ITypedHubClient> hubContext, IRepository<Blog> blogRepository)
        {
            _commentRepository = commentRepository;
            _heroRepository = heroRepository;
            _userRepository = userRepository;
            _hubContext = hubContext;
            _blogRepository = blogRepository;
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return this._commentRepository.All().ToList();
        }

        public async Task CreateComment(CreateCommentDTO commentDto)
        {

            var userObj = this._userRepository.All().FirstOrDefault(x => x.Id == commentDto.UserId);


            if (commentDto.Action == "Hero")
            {
                var heroObj = this._heroRepository.All().FirstOrDefault(x => x.Id == commentDto.HeroId);

                var commentObj = new Comment
                {
                    Text = commentDto.Comment,
                    UserName = userObj.UserName,
                    ProfileImage = userObj.ProfileImage,
                    HeroId = heroObj.Id,
                    UserId = userObj.Id
                };

                await _hubContext.Clients.All.BroadcastComment(commentObj);

                heroObj.Comments.Add(commentObj);
                userObj.Comments.Add(commentObj);

                await this._heroRepository.SaveChangesAsync();
            }
            else
            {
                var blogObj = this._blogRepository.All().FirstOrDefault(x => x.Id == commentDto.HeroId);

                var commentObj = new Comment
                {
                    Text = commentDto.Comment,
                    UserName = userObj.UserName,
                    ProfileImage = userObj.ProfileImage,
                    BlogId = blogObj.Id,
                    UserId = userObj.Id
                };

                await _hubContext.Clients.All.BroadcastComment(commentObj);

                blogObj.Comments.Add(commentObj);
                userObj.Comments.Add(commentObj);

                await this._blogRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteComment(int id)
        {
            var commentToDelete = this._commentRepository.All().FirstOrDefault(x => x.Id == id);

            if (commentToDelete != null) commentToDelete.IsDeleted = true;

            await _hubContext.Clients.All.DeleteComment(id);

            await this._commentRepository.SaveChangesAsync();
        }
    }
}