using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace KinderKulturServer
{
    /// <summary>
    /// .NET Core Entry Class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// .NET Core Entry Method
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}