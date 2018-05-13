using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ConsoleParking;

namespace ParkingWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if(File.Exists(Parking.Instance.Settings.LogPath))
                File.Delete(Parking.Instance.Settings.LogPath);
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
