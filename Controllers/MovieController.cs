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
        private readonly IPhotoService _photoService;
        public MovieController(IMovieRepository movieRepository, IPhotoService photoService)
        {
            _movieRepository = movieRepository;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Models.Movie> movies = await _movieRepository.GetAllMovies();
            return View(movies);
        }
        public async Task<IActionResult> Add()
        {
            IEnumerable<Genre> genres = await _movieRepository.GetAllGenres();
            var movieVM = new AddMovieViewModel
            {
                Genres = genres,
            };
            return View(movieVM);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddMovieViewModel movieVM)
        {
            if(ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(movieVM.Poster);
                var selectedGenres = await _movieRepository.GetSelectedGenres(movieVM.SelectedGenres);
                var movie = new Models.Movie
                {
                    Title = movieVM.Title,
                    Description = movieVM.Description,
                    Poster = result.Url.ToString(),
                    State = "Yes",
                    Revenue = 0,
                    Genres = selectedGenres
                };
                _movieRepository.AddMovie(movie);

                return RedirectToAction("Index");
            }

            return View(movieVM);
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
