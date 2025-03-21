using Movie.Models;

namespace Movie.Interfaces
{
    public interface ISeatRepository
    {
        bool AddSeatsFromShowtime(Seat seat);
        Task<IEnumerable<Seat>> GetSeatsFromShowtime(int showtimeId);
        Task<IEnumerable<Seat>> GetSeatsFromShowtimeAndPlace(int showtimeId, string place);
        Task<IEnumerable<Seat>> GetSeatsFromUserId(string userId);
        bool Update(Seat seat);
    }
}
