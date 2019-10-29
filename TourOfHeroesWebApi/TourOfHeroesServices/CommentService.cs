namespace TourOfHeroesServices
{
    using Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TourOfHeroesData.Common.Contracts;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs;
    using Microsoft.AspNetCore.SignalR;
    using RealTimeHub;

    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<Hero> _heroRepository;
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IHubContext<CommentHub, ITypedHubClient> _hubContext;

        public CommentService(IRepository<Comment> commentRepository, IRepository<Hero> heroRepository, IRepository<ApplicationUser> userRepository, IHubContext<CommentHub, ITypedHubClient> hubContext)
        {
            _commentRepository = commentRepository;
            _heroRepository = heroRepository;
            _userRepository = userRepository;
            _hubContext = hubContext;
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return this._commentRepository.All().ToList();
        }

        public async Task CreateComment(CreateCommentDTO commentDTO)
        {
            var heroObj = this._heroRepository.All().FirstOrDefault(x => x.Id == commentDTO.HeroId);

            var userObj = this._userRepository.All().FirstOrDefault(x => x.Id == commentDTO.UserId);

            if (heroObj != null)
            {
                if (userObj.ProfileImage == null)
                {
                    var commentObj = new Comment
                    {
                        Text = commentDTO.Comment,
                        UserName = userObj.UserName,
                        ProfileImage = "https://icon-library.net/images/no-profile-picture-icon/no-profile-picture-icon-7.jpg",
                        HeroId = heroObj.Id,
                        UserId = userObj.Id
                    };


                    await _hubContext.Clients.All.BroadcastComment(commentObj);

                    heroObj.Comments.Add(commentObj);
                    userObj.Comments.Add(commentObj);
                }
                else
                {
                    var commentObj = new Comment
                    {
                        Text = commentDTO.Comment,
                        UserName = userObj.UserName,
                        ProfileImage = userObj.ProfileImage,
                        HeroId = heroObj.Id,
                        UserId = userObj.Id
                    };

                    await _hubContext.Clients.All.BroadcastComment(commentObj);

                    heroObj.Comments.Add(commentObj);
                    userObj.Comments.Add(commentObj);
                }

                await this._heroRepository.SaveChangesAsync();
                await this._userRepository.SaveChangesAsync();
                await this._commentRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteComment(int id)
        {
            var commentToDelete = this._commentRepository.All().FirstOrDefault(x => x.Id == id);

            if (commentToDelete != null) commentToDelete.IsDeleted = true;

            await this._commentRepository.SaveChangesAsync();
        }
    }
}