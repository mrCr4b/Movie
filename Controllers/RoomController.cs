using Microsoft.AspNetCore.Mvc;
using Movie.Interfaces;
using Movie.Models;
using Movie.Models.ViewModels;

namespace Movie.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Room> rooms = await _roomRepository.GetAll();
            return View(rooms);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddRoomViewModel roomVM)
        {
            if (ModelState.IsValid)
            {
                var room = new Room
                {
                    Name = roomVM.Name,
                    Capacity = roomVM.Capacity
                };
                _roomRepository.Add(room);

                return RedirectToAction("Index");
            }

            return View(roomVM);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            var roomVM = new AddRoomViewModel
            {
                Name = room.Name,
                Capacity = (int)room.Capacity
            };

            return View(roomVM);
        }
    }
}
