using Movie.Models;

namespace Movie.Interfaces
{
    public interface IRoomRepository
    {
        Task<Room> GetByIdAsync(int id);
        Task<IEnumerable<Room>> GetAll();
        bool Add(Room room);
        bool Update(Room room);
    }
}
