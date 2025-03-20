using Microsoft.AspNetCore.Mvc;
using Movie.Interfaces;
using Movie.Models;
using Movie.Models.ViewModels;


namespace Movie.Controllers
{
    public class ShowtimeController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IShowtimeRepository _showtimeRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ShowtimeController(IMovieRepository movieRepository, IRoomRepository roomRepository, IShowtimeRepository showtimeRepository, ISeatRepository seatRepository, IHttpContextAccessor httpContextAccessor)
        {
            _movieRepository = movieRepository;
            _roomRepository = roomRepository;
            _showtimeRepository = showtimeRepository;
            _seatRepository = seatRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Showtime> showtimes = await _showtimeRepository.GetAll();
            return View(showtimes);
        }
        public async Task<IActionResult> Add()
        {
            IEnumerable<Models.Movie> movies = await _movieRepository.GetAllMovies();
            IEnumerable<Room> rooms = await _roomRepository.GetAll();
            var showtimeVM = new AddShowtimeViewModel
            {
                Rooms = rooms,
                Movies = movies
            };

            return View(showtimeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddShowtimeViewModel showtimeVM)
        {
            if(ModelState.IsValid)
            {
                var showtime = new Showtime
                {
                    MovieId = showtimeVM.SelectedMovieId,
                    RoomId = showtimeVM.SelectedRoomId,
                    Time = showtimeVM.Time,
                    TicketPrice = showtimeVM.TicketPrice
                };
                _showtimeRepository.Add(showtime);
                // Thêm dữ liệu vào bảng 'Seats'
                var latestShowtime = await _showtimeRepository.GetLatest();
                var roomCapacity = latestShowtime.Room.Capacity;
                for(var i = 0; i < roomCapacity;  i++)
                {
                    var seat = new Seat
                    {
                        ShowtimeId = latestShowtime.Id,
                        Place = i.ToString(),
                        State = "None",
                        UserId = "None"
                    };
                    _seatRepository.AddSeatsFromShowtime(seat);
                }
                return RedirectToAction("Index");
            }

            return View(showtimeVM);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var showtime = await _showtimeRepository.GetById(id);
            IEnumerable<Seat> seats = await _seatRepository.GetSeatsFromShowtime(id);
            IEnumerable<ReserveSeatsViewModel> seatsVM = seats
            .Select(s => new ReserveSeatsViewModel
            {
                ShowtimeId = s.ShowtimeId,
                Place = s.Place,
                State = s.State,
                UserId = curUserId
            });

            return View(seatsVM);
        }
    }
}
