using Microsoft.AspNetCore.Mvc;
using Movie.Models.ViewModels;
using Movie.Models;
using Movie.Interfaces;
using Movie.Repositories;

namespace Movie.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        public async Task<IActionResult> GenreIndex()
        {
            IEnumerable<Genre> genres = await _movieRepository.GetAllGenres();
            return View(genres);
        }
        public IActionResult AddGenre()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddGenre(AddGenreViewModel genreVM)
        {
            if (ModelState.IsValid)
            {
                var genre = new Genre
                {
                    Name = genreVM.Name
                };
                _movieRepository.AddGenre(genre);

                return RedirectToAction("GenreIndex");
            }

            return View(genreVM);
        }
    }
}
