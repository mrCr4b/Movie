using Movie.Models;

namespace Movie.Interfaces
{
    public interface ISeatRepository
    {
        bool AddSeatsFromShowtime(Seat seat);
    }
}
