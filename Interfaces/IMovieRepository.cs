using Movie.Models;

namespace Movie.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Genre>> GetAllGenres();
        Task<IEnumerable<Models.Movie>> GetAllMovies();
        bool AddGenre(Genre genre);
        bool AddMovie(Models.Movie movie);
        Task<List<Genre>> GetSelectedGenres(List<int> selectedGenres); 
    }
}
