namespace TourOfHeroesServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TourOfHeroesData.Common.Contracts;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.HeroDtos;
    using TourOfHeroesMapping.Mapping;

    using Contracts;

    public class HeroService : IHeroService
    {
        private readonly IRepository<Hero> _heroRepository;
        private readonly IImageService _imageService;
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IRepository<Comment> _commentRepository;

        public HeroService(IRepository<Hero> heroRepository, IImageService imageService, IRepository<ApplicationUser> userRepository, IRepository<Comment> commentRepository)
        {
            _heroRepository = heroRepository;
            _imageService = imageService;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        public IEnumerable<GetHeroDTO> GetAllHeroes()
        {
            var allHeroes = _heroRepository
                .All()
                .To<GetHeroDTO>()
                .ToList();

            return allHeroes;
        }

        public GetHeroDetailDTO GetById(string currentUser, int id)
        {
            var hero = this._heroRepository
                .All()
                .To<GetHeroDetailDTO>()
                .Single(x => x.Id == id);

            hero.CurrentUser = this._userRepository.All().Single(x => x.Id == currentUser).UserName;

            return hero;
        }

        public IEnumerable<GetHeroDTO> GetHeroBySearchString(string name)
        {
            return this._heroRepository
                .All()
                .Where(x => x.Name.Contains(name))
                .To<GetHeroDTO>()
                .ToList();
        }

        public async Task CreateHero(CreateHeroDTO hero, bool skipAddToCloudinaryMethod = false)
        {
            string imgUrl = "";
            string coverImgUrl = "";

            if (string.IsNullOrEmpty(hero.Description) || string.IsNullOrEmpty(hero.Gender) ||
                string.IsNullOrEmpty(hero.Name) || string.IsNullOrEmpty(hero.RealName) ||
                hero.Image == null || hero.CoverImage == null)
            {
                throw new InvalidOperationException("Fields cannot be null or empty.");
            }

            //Skip this method for unit testing
            if (!skipAddToCloudinaryMethod)
            {
                imgUrl = this._imageService.AddToCloudinaryAndReturnHeroImageUrl(hero.Image);
                coverImgUrl = this._imageService.AddToCloudinaryAndReturnHeroImageUrl(hero.CoverImage);
            }

            var heroObj = new Hero
            {
                Name = hero.Name,
                Description = hero.Description,
                Image = imgUrl,
                CoverImage = coverImgUrl,
                RealName = hero.RealName,
                Birthday = hero.Birthday.Date,
                Gender = hero.Gender
            };

            var movieTitleList = new List<Movie>();
            if (hero.MovieTitle.Length > 0 && hero.MovieTitle != null)
            {
                //Replacing and removing all the symbols that prevent the 'movie title' come how its expected.
                var movieTitles = hero.MovieTitle[0].Replace("\"", "").Replace("\\", "").TrimStart(' ', '"', ']', '\\', '/', '[').TrimEnd(' ', '"', ']', '\\', '/', '[').Split(",", StringSplitOptions.RemoveEmptyEntries);
                foreach (var title in movieTitles)
                {
                    var movieTitle = new Movie
                    {
                        Title = title,
                        HeroId = heroObj.Id
                    };
                    movieTitleList.Add(movieTitle);
                }

                heroObj.Movies = movieTitleList;
            }

            await this._heroRepository.AddAsync(heroObj);

            await this._heroRepository.SaveChangesAsync();
        }

        public async Task UpdateHero(int id, UpdateHeroDTO hero)
        {
            var dbHero = this._heroRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            if (dbHero != null)
            {
                var editHistory = new EditHistory()
                {
                    OldValue = dbHero.Name,
                    NewValue = hero.Name,
                    HeroId = dbHero.Id
                };

                dbHero.EditHistory.Add(editHistory);

                dbHero.Name = hero.Name;
            }

            await this._heroRepository.SaveChangesAsync();
        }

        public async Task DeleteHero(int id)
        {
            var heroToDelete = this._heroRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            var commentsToDelete = this._commentRepository
                .All()
                .Where(x => x.HeroId == id)
                .ToList();

            foreach (var comment in commentsToDelete)
            {
                this._commentRepository.Delete(comment);
            }

            this._heroRepository.Delete(heroToDelete);

            await this._heroRepository.SaveChangesAsync();
        }
    }
}