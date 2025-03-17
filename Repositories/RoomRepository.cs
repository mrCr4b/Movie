using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Movie.Interfaces;
using Movie.Models;

namespace Movie.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly MovieContext _context;
        public RoomRepository(MovieContext context)
        {
            _context = context;
        }
        public bool Add(Room room)
        {
            _context.Add(room);
            return Save();
        }

        public async Task<IEnumerable<Room>> GetAll()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _context.Rooms.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Room room)
        {
            _context.Update(room);
            return Save();
        }
    }
}
