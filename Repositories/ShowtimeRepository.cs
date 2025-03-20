using Microsoft.EntityFrameworkCore;
using Movie.Interfaces;
using Movie.Models;

namespace Movie.Repositories
{
    public class ShowtimeRepository : IShowtimeRepository
    {
        private readonly MovieContext _context;
        public ShowtimeRepository(MovieContext context)
        {
            _context = context;
        }
        public bool Add(Showtime showtime)
        {
            _context.Add(showtime);
            return Save();
        }

        public async Task<IEnumerable<Showtime>> GetAll()
        {
            return await _context.Showtimes.Include(s => s.Room).Include(s => s.Movie).ToListAsync();
        }

        public async Task<Showtime> GetById(int id)
        {
            return await _context.Showtimes.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Showtime> GetLatest()
        {
            return await _context.Showtimes.Include(s => s.Room).OrderByDescending(m => m.Id).FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
