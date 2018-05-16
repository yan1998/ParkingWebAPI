using Microsoft.AspNetCore.Mvc;
using ConsoleParking;

namespace ParkingWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ParkingController : Controller
    {
        // GET : api/parking/FreePlaces
        [HttpGet]
        public JsonResult FreePlaces()
        {
            var free = new { freePlaces = Parking.Instance.CountFreePlaces };
            return Json(free);
        }

        // GET : api/parking/OccupiedPlaces
        [HttpGet]
        public JsonResult OccupiedPlaces()
        {
            var occupiedPlaces = new { occupiedPlaces = Parking.Instance.Cars.Count };
            return Json(occupiedPlaces);
        }

        // GET : api/parking/Balance
        [HttpGet]
        public JsonResult Balance()
        {
            var balance = new { balance = Parking.Instance.Balance };
            return Json(balance);
        }
    }
}
