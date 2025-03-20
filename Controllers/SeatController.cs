using Microsoft.AspNetCore.Mvc;

namespace Movie.Controllers
{
    public class SeatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
