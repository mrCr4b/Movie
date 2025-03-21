using Microsoft.AspNetCore.Mvc;
using Movie.Interfaces;
using Movie.Models;
using Movie.Models.ViewModels;

namespace Movie.Controllers
{
    public class SeatController : Controller
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SeatController(ISeatRepository seatRepository, IHttpContextAccessor httpContextAccessor)
        {
            _seatRepository = seatRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost]
        public async Task<IActionResult> TakeSeats(int showtimeId, List<string> SelectedPlaces, string UserId)
        {
            if(ModelState.IsValid)
            {
                // Lấy danh sách seat dựa vào showtimeId
                IEnumerable<Seat> seats = await _seatRepository.GetSeatsFromShowtime(showtimeId);
                // Quét danh sách đó và chỉnh sửa 
                // Convert SelectedPlaces to HashSet for faster lookup
                HashSet<string> selectedPlacesSet = new HashSet<string>(SelectedPlaces);

                // Cập nhật ghế đã chọn
                foreach (var seat in seats)
                {
                    if (selectedPlacesSet.Contains(seat.Place))
                    {
                        seat.State = "Yes"; // Đánh dấu ghế đã được đặt
                        seat.UserId = UserId; // Gán UserId
                        _seatRepository.Update(seat); // Cập nhật trực tiếp trên thực thể được theo dõi
                    }
                }

                // Trừ tiền của user

                // Cộng vào doanh thu của phim

                return RedirectToAction("Index", "Showtime");
            }
            return RedirectToAction("Index", "Movie");
        }
        public async Task<IActionResult> SeatsByUser()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            IEnumerable<Seat> seats = await _seatRepository.GetSeatsFromUserId(curUserId);
            return View(seats);
        }
        public async Task<IActionResult> CancelSeats(string place, int showtimeId)
        {
            IEnumerable<Seat> seats = await _seatRepository.GetSeatsFromShowtimeAndPlace(showtimeId, place);
            foreach (var seat in seats)
            {
                seat.State = "None"; // Hủy ghế đã được đặt
                seat.UserId = "None"; // Xóa UserId
                _seatRepository.Update(seat); // Cập nhật trực tiếp trên thực thể được theo dõi
            }
            return RedirectToAction("SeatsByUser", "Seat");

            // Trừ bớt doanh thu của phim
        }
    }
}
