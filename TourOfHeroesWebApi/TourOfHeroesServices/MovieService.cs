namespace TourOfHeroesServices
{
    using System.Linq;
    using TourOfHeroesData.Common.Contracts;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TourOfHeroesData.Models;
    using Contracts;
    using TourOfHeroesDTOs.MovieDtos;

    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;

        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
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

        public Task LikeMovie(AddToLikesMovieDTO movieDTO)
        {
            throw new System.NotImplementedException();
        }
    }
}