using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BookReserveWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //new DataBaseClearAndFill().ClearAndFill();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
