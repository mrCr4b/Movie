using Microsoft.EntityFrameworkCore;
using Movie.Interfaces;
using Movie.Models;

namespace Movie.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;
        public MovieRepository(MovieContext context)
        {
            _context = context;
        }

        public bool AddGenre(Genre genre)
        {
            _context.Add(genre);
            return Save();
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await _context.Genres.ToListAsync();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
