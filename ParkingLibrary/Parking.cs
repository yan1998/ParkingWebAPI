using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Timers;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace ConsoleParking
{
    public class Parking:IDisposable
    {
        public class TransactionLog
        {
            public string Date { get; set; }
            public double Sum { get; set; }
        }

        private object locker = new object();

        /// <summary>
        /// Instance of singleton object
        /// </summary>
        private static Lazy<Parking> parking { get; set; } = new Lazy<Parking>(() => new Parking());

        /// <summary>
        /// Property for get singleton object
        /// </summary>
        public static Parking Instance
        {
            get { return parking.Value; }
        }

        private bool disposed = false;
        private Timer timer = new Timer(60000);
        private double balance = 0;

        private Parking()
        {
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            List<TransactionLog> transactions = new List<TransactionLog>();
            if (File.Exists(Settings.LogPath))
            {
                transactions = JsonConvert.DeserializeObject<List<TransactionLog>>(File.ReadAllText(Settings.LogPath));
                File.Delete(Settings.LogPath);
            }
            double sum = Transactions.Where(tra => tra.DateTime.AddMinutes(1) >= DateTime.Now).Sum(tra => tra.WriteOff);
            transactions.Add(new TransactionLog() { Sum = sum, Date = DateTime.Now.ToString() });
            string newJson = JsonConvert.SerializeObject(transactions);

            File.WriteAllText(Settings.LogPath, newJson);
        }

        /// <summary>
        /// Get parking settings 
        /// </summary>
        public Settings Settings { get { return Settings.Instance; } }

        /// <summary>
        /// Get list of cars which staying in parking
        /// </summary>
        public List<Car> Cars { get; } = new List<Car>();

        /// <summary>
        /// Get list of transactions
        /// </summary>
        public ConcurrentBag<Transaction> Transactions { get; } = new ConcurrentBag<Transaction>();

        /// <summary>
        /// Show parking income
        /// </summary>
        public double Balance
        {
            get
            {
                lock (locker)
                {
                    return parking.Value.balance;
                }
            }
            set
            {
                lock (locker)
                {
                    if (value > 0)
                        parking.Value.balance = value;
                    else
                        throw new Exception("Баланс не может быть меньше нуля!");
                }
            }
        }

        public int CountFreePlaces { get { return Settings.ParkingSpace - Cars.Count; } }

        public List<TransactionLog> GetContainLogFile()
        {
            List<TransactionLog> transactions = null;
            if (File.Exists(Settings.LogPath))
            {
                transactions= JsonConvert.DeserializeObject<List<TransactionLog>>(File.ReadAllText(Settings.LogPath));
            }
            return transactions;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
                timer.Dispose();

            disposed = true;
        }
    }
}
