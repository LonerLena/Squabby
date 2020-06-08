using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Squabby.Helpers.Config;

namespace Squabby
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(ConfigHelper.GetConfig().ConnectionString);
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseUrls(ConfigHelper.GetConfig().ServerEndPoints);
                });
    }
}