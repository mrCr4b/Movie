using Movie.Models;

namespace Movie.Interfaces
{
    public interface IShowtimeRepository
    {
        bool Add(Showtime showtime);
        Task<Showtime> GetLatest();
        Task<IEnumerable<Showtime>> GetAll();
    }
}
