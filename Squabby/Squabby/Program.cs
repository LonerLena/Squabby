using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Squabby.Helpers.Config;

namespace Squabby
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseUrls(ConfigHelper.GetConfig().ServerEndPoints);
                });
        }
    }
}