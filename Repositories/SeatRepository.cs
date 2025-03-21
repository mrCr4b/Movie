using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Seat>> GetSeatsFromShowtime(int showtimeId)
        {
            return await _context.Seats.Where(s => s.ShowtimeId == showtimeId).ToListAsync();
        }

        public async Task<IEnumerable<Seat>> GetSeatsFromShowtimeAndPlace(int showtimeId, string place)
        {
            return await _context.Seats.Where(s => s.ShowtimeId == showtimeId && s.Place == place).ToListAsync();
        }

        public async Task<IEnumerable<Seat>> GetSeatsFromUserId(string userId)
        {
            return await _context.Seats
                .Where(s => s.UserId == userId)
                .Include(s => s.Showtime)
                    .ThenInclude(s => s.Movie)
                .Include(s => s.Showtime)
                    .ThenInclude(s => s.Room)
                .ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool Update(Seat seat)
        {
            _context.Update(seat);
            return Save();
        }
    }
}
