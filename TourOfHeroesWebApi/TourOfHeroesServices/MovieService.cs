namespace TourOfHeroesServices
{
    using System.Linq;
    using TourOfHeroesData.Common.Contracts;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TourOfHeroesData.Models;
    using System;
    using System.Globalization;
    using Contracts;
    using TourOfHeroesDTOs.MovieDtos;
    using TourOfHeroesMapping.Mapping;

    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;
        private readonly IRepository<LikedMovie> _likedMovieRepository;
        private readonly IRepository<ApplicationUser> _userRepository;

        public MovieService(IRepository<Movie> movieRepository, IRepository<LikedMovie> likedMovieRepository, IRepository<ApplicationUser> userRepository)
        {
            _movieRepository = movieRepository;
            _likedMovieRepository = likedMovieRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return this._movieRepository.All().ToList();
        }

        public IEnumerable<GetLikedMovieDTO> GetLikedMovies(string userId)
        {
            return this._likedMovieRepository
                .All()
                .Where(x => x.UserId == userId)
                .To<GetLikedMovieDTO>()
                .ToList();
        }

        public async Task DeleteMovie(string title)
        {
            var movieToDelete = this._movieRepository.All().FirstOrDefault(x => x.Title == title);

            this._movieRepository.Delete(movieToDelete);

            await this._movieRepository.SaveChangesAsync();
        }

        public async Task LikeMovie(AddToLikesMovieDTO movieDTO)
        {
            var movieToLike = new LikedMovie
            {
                PosterPath = movieDTO.PosterPath,
                ReleaseDate = DateTime.ParseExact(movieDTO.ReleaseDate, "yyyy-MM-dd", CultureInfo.CurrentCulture),
                Title = movieDTO.Title,
                UserId = movieDTO.UserId,
                VoteAverage = double.Parse(movieDTO.VoteAverage),
                VoteCount = int.Parse(movieDTO.VoteCount)
            };

            this._userRepository
                .All()
                .FirstOrDefault(u => u.Id == movieDTO.UserId)
                ?.LikedMovies.Add(movieToLike);

            await this._userRepository.SaveChangesAsync();
            await this._likedMovieRepository.AddAsync(movieToLike);
            await this._likedMovieRepository.SaveChangesAsync();
        }

        public async Task DislikeMovie(int id)
        {
            //TODO: IMPLEMENT THE RIGHT LOGIC FLOW FOR DISLIKE MOVIE
            var movieToDislike = this._likedMovieRepository.All().FirstOrDefault(x => x.Id == id);

            this._likedMovieRepository.Delete(movieToDislike);

            await this._movieRepository.SaveChangesAsync();
        }
    }
}