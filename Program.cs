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
                .UseStartup<Startup>()
                .ConfigureKestrel((context, options) =>
        {
            // Set properties and call methods on options // ToDo NetCore 2.2
        });
    }
}