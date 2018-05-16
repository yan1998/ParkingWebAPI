using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConsoleParking;

namespace ParkingWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CarsController : Controller
    {
        // GET : api/cars
        [HttpGet]
        public JsonResult GetCars()
        {
            return Json(Parking.Instance.Cars.ToList<Car>());
        }

        // GET : api/cars/number
        [HttpGet("{number}")]
        public JsonResult GetCar(string number)
        {
            Car car = Parking.Instance.Cars.Find((c)=> { return c.CarNumber == number; });
            if (car == null)
                 HttpContext.Response.StatusCode = 404;
            return Json(car);
        }

        // POST : api/cars
        [HttpPost]
        public JsonResult AddCar([FromBody]Car car)
        {
            try
            {
                car.AddToParking(Parking.Instance);
                HttpContext.Response.StatusCode = 201;  //Добавлено 
            }
            catch (System.Exception ex)
            {
                HttpContext.Response.StatusCode = 406; //Неприемлимо
                return Json(ex.Message);
            }
            return Json(car);
        }

        // DELETE : api/cars/{number}
        [HttpDelete("{number}")]
        public void DeleteCar(string number)
        {
            Car car = Parking.Instance.Cars.Find((c) => { return c.CarNumber == number; });
            if(car==null)
            {
                HttpContext.Response.StatusCode = 404;
                return;
            }
            try
            {
                car.RemoveFromParking(Parking.Instance);
            }
            catch (System.Exception)
            {
                HttpContext.Response.StatusCode = 402;  //Необходима оплата
                return;
            }
            HttpContext.Response.StatusCode = 204;
        }
    }
}
