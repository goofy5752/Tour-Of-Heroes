namespace TourOfHeroesServices.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TourOfHeroesData.Models;

    public interface IMovieService
    {
        IEnumerable<Movie> GetAllMovies();

        Task DeleteMovie(string title);
    }
}