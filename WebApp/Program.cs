using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;


namespace WebApp
{
    public class Program
    {
        private static string URL { get; set; }

        public static void Main(string[] args)
        {
            try
            {
                URL = File.ReadAllText("Config.txt");
                CreateWebHostBuilder(args)
                    .UseUrls(URL)
                    .Build().Run();
            }
            catch (Exception)
            {
                CreateWebHostBuilder(args)
                    .Build().Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
