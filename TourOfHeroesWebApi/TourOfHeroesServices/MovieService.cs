namespace TourOfHeroesServices
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Globalization;

    using TourOfHeroesData.Common.Contracts;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.MovieDtos;
    using TourOfHeroesMapping.Mapping;

    using Contracts;

    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;
        private readonly IRepository<LikedMovie> _likedMovieRepository;
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IRepository<UserActivity> _userActivityRepository;

        public MovieService(IRepository<Movie> movieRepository, IRepository<LikedMovie> likedMovieRepository, IRepository<ApplicationUser> userRepository, IRepository<UserActivity> userActivityRepository)
        {
            _movieRepository = movieRepository;
            _likedMovieRepository = likedMovieRepository;
            _userRepository = userRepository;
            _userActivityRepository = userActivityRepository;
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
            var movieToDelete = this._movieRepository
                .All()
                .Single(x => x.Title == title);

            this._movieRepository.Delete(movieToDelete);

            await this._movieRepository.SaveChangesAsync();
        }

        public async Task LikeMovie(AddToLikesMovieDTO movieDTO)
        {
            if (string.IsNullOrEmpty(movieDTO.Title) ||
                string.IsNullOrEmpty(movieDTO.PosterPath) ||
                string.IsNullOrEmpty(movieDTO.ReleaseDate) ||
                string.IsNullOrEmpty(movieDTO.VoteAverage) ||
                string.IsNullOrEmpty(movieDTO.VoteCount))
            {
                throw new InvalidOperationException("Invalid movie to like.");
            }

            var user = this._userRepository
                .All()
                .Single(u => u.Id == movieDTO.UserId);

            var movieToLike = new LikedMovie
            {
                PosterPath = movieDTO.PosterPath,
                ReleaseDate = DateTime.ParseExact(movieDTO.ReleaseDate, "yyyy-MM-dd", CultureInfo.CurrentCulture),
                Title = movieDTO.Title,
                UserId = movieDTO.UserId,
                VoteAverage = double.Parse(movieDTO.VoteAverage),
                VoteCount = int.Parse(movieDTO.VoteCount)
            };

            var likedMovies = this._likedMovieRepository
                .All()
                .Where(x => x.UserId == movieDTO.UserId)
                .ToList();

            if (likedMovies == null || likedMovies.Any(x => x.Title == movieToLike.Title && x.PosterPath == movieToLike.PosterPath))
            {
                throw new InvalidOperationException("Movie is already liked.");
            }

            var activity = new UserActivity
            {
                Action = $"Like movie with title '{movieToLike.Title}'",
                RegisteredOn = DateTime.Now,
                UserId = movieDTO.UserId,
            };

            await this._likedMovieRepository.AddAsync(movieToLike);
            await this._userActivityRepository.AddAsync(activity);

            user?.LikedMovies.Add(movieToLike);
            user?.Activity.Add(activity);

            await this._userRepository.SaveChangesAsync();
        }

        public async Task DislikeMovie(int id)
        {
            var movieToDislike = this._likedMovieRepository
                .All()
                .Single(x => x.Id == id);

            var activity = new UserActivity
            {
                Action = $"Dislike movie with title '{movieToDislike?.Title}'",
                RegisteredOn = DateTime.Now,
                UserId = movieToDislike?.UserId,
            };

            await this._userActivityRepository.AddAsync(activity);

            this._userRepository
                .All()
                .Single(x => x.Id == movieToDislike.UserId)
                .Activity
                .Add(activity);

            this._likedMovieRepository.Delete(movieToDislike);

            await this._likedMovieRepository.SaveChangesAsync();
        }
    }
}