namespace TourOfHeroesServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TourOfHeroesData.Models;
    using TourOfHeroesDTOs.MovieDtos;

    public interface IMovieService
    {
        IEnumerable<Movie> GetAllMovies();

        IEnumerable<GetLikedMovieDTO> GetLikedMovies(string userId);

        Task DeleteMovie(string title);

        Task LikeMovie(AddToLikesMovieDTO movieDTO);

        Task DislikeMovie(int id);
    }
}