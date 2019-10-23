using TourOfHeroesDTOs;

namespace TourOfHeroesServices
{
    using Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TourOfHeroesData.Common.Contracts;
    using TourOfHeroesData.Models;

    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<Hero> _heroRepository;
        private readonly IRepository<ApplicationUser> _userRepository;

        public CommentService(IRepository<Comment> commentRepository, IRepository<Hero> heroRepository, IRepository<ApplicationUser> userRepository)
        {
            _commentRepository = commentRepository;
            _heroRepository = heroRepository;
            _userRepository = userRepository;
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
                var commentObj = new Comment
                {
                    Text = commentDTO.Comment,
                    HeroId = heroObj.Id,
                    UserId = userObj.Id
                };

                heroObj.Comments.Add(commentObj);
                userObj.Comments.Add(commentObj);

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