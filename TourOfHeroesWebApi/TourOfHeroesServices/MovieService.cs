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

    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;
        private readonly IRepository<LikedMovie> _likedMovieRepository;

        public MovieService(IRepository<Movie> movieRepository, IRepository<LikedMovie> likedMovieRepository)
        {
            _movieRepository = movieRepository;
            _likedMovieRepository = likedMovieRepository;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return this._movieRepository.All().ToList();
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
                ReleaseDate = DateTime.ParseExact(movieDTO.ReleaseDate, "mm/dd/yyy", CultureInfo.InvariantCulture),
                Title = movieDTO.Title,
                UserId = movieDTO.UserId,
                VoteAverage = movieDTO.VoteAverage,
                VoteCount = movieDTO.VoteCount
            };

            await this._likedMovieRepository.AddAsync(movieToLike);
            await this._likedMovieRepository.SaveChangesAsync();
        }
    }
}