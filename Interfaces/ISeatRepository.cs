using Movie.Models;

namespace Movie.Interfaces
{
    public interface ISeatRepository
    {
        bool AddSeatsFromShowtime(Seat seat);
        Task<IEnumerable<Seat>> GetSeatsFromShowtime(int showtimeId);
        bool Update(Seat seat);
    }
}
