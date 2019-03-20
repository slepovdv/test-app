using JobsApp.Migrations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace JobsApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            var migrator = host.Services.GetService(typeof(Migrator)) as Migrator;
            if(migrator != null)
            {
                migrator.Run();
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
