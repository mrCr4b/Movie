using Movie.Models;

namespace Movie.Interfaces
{
    public interface IMovieRepository
    {

        Task<IEnumerable<Genre>> GetAllGenres();
        bool AddGenre(Genre genre);
    }
}
