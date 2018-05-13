using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConsoleParking;

namespace ParkingWebAPI.Controllers
{
    [Produces("application/json")]
    public class CarsController : Controller
    {
        // GET : api/Cars/GetCars
        [HttpGet]
        public JsonResult GetCars()
        {
            return Json(Parking.Instance.Cars.ToList<Car>());
        }

        // GET : api/Cars/GetCar/{number}
        [HttpGet]
        public JsonResult GetCar(string number)
        {
            Car car = Parking.Instance.Cars.Find((c)=> { return c.CarNumber == number; });
            if (car == null)
                 HttpContext.Response.StatusCode = 404;
            return Json(car);
        }

        // POST : api/Cars/AddCar
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

        // DELETE : api/Cars/DeleteCar/{number}
        [HttpDelete]
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
            HttpContext.Response.StatusCode = 200;
        }
    }
}
