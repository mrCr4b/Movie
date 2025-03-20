using Movie.Interfaces;
using Movie.Models;

namespace Movie.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly MovieContext _context;
        public SeatRepository(MovieContext context)
        {
            _context = context;
        }
        public bool AddSeatsFromShowtime(Seat seat)
        {
            _context.Add(seat);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
