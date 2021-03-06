﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConsoleParking;

namespace ParkingWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class TransactionsController : Controller
    {

        // GET : api/transactions/TransactionLog/
        [HttpGet]
        public JsonResult TransactionLog()
        {
            List<Parking.TransactionLog> log=Parking.Instance.GetContainLogFile();
            return Json(log);
        }

        // GET : api/transactions/Transactions/
        [HttpGet]
        public JsonResult Transactions()
        {
            List<Transaction> transactions = Parking.Instance.Transactions.Where(tr =>{ return tr.DateTime.AddMinutes(1) >= DateTime.Now;}).ToList<Transaction>();
            return Json(transactions);
        }

        // GET : api/transactions/CarTransactions/{number}
        [HttpGet("{number}")]
        public JsonResult CarTransactions(string number)
        {
            List<Transaction> transactions = Parking.Instance.Transactions.Where(tr => { return tr.DateTime.AddMinutes(1) >= DateTime.Now && tr.CarNumber==number; }).ToList<Transaction>();
            if (transactions.Count == 0)
                HttpContext.Response.StatusCode = 404;
            return Json(transactions);
        }

        // PUT: api/transactions/AddCarBalance/{number}
        [HttpPut("{number}")]
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
