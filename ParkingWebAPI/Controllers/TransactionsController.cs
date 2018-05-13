using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConsoleParking;

namespace ParkingWebAPI.Controllers
{
    [Produces("application/json")]
    public class TransactionsController : Controller
    {
        public class AddCoins
        {
            public double Value{ get; set; }
        }

        // GET : api/Transactions/GetTransactionLog/
        [HttpGet]
        public JsonResult GetTransactionLog()
        {
            List<Parking.TransactionLog> log=Parking.Instance.GetContainLogFile();
            return Json(log);
        }

        // GET : api/Transactions/GetTransactions/
        [HttpGet]
        public JsonResult GetTransactions()
        {
            List<Transaction> transactions = Parking.Instance.Transactions.Where(tr =>{ return tr.DateTime.AddMinutes(1) >= DateTime.Now;}).ToList<Transaction>();
            return Json(transactions);
        }

        // GET : api/Transactions/GetCarTransactions/{number}
        [HttpGet]
        public JsonResult GetCarTransactions(string number)
        {
            List<Transaction> transactions = Parking.Instance.Transactions.Where(tr => { return tr.DateTime.AddMinutes(1) >= DateTime.Now && tr.CarNumber==number; }).ToList<Transaction>();
            if (transactions.Count == 0)
                HttpContext.Response.StatusCode = 404;
            return Json(transactions);
        }

        // PUT: api/Transactions/AddCarBalance/{number}
        [HttpPut]
        public JsonResult AddCarBalance(string number, [FromBody]AddCoins coins)
        {
            Car car = Parking.Instance.Cars.Find((c) => { return c.CarNumber == number; });
            if (car == null)
            {
                HttpContext.Response.StatusCode = 404;
                return null;
            }
            car.Balance += coins.Value;
            var newBalance = new{ newBalance=car.Balance};
            return Json(newBalance);
        }

    }
}
