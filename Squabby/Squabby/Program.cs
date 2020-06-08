using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Squabby
{
    public class Program
    {
         public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseUrls("http://0.0.0.0:5001");
                });
    }
}