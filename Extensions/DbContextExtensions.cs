using KinderKulturServer.Data;
using KinderKulturServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KinderKulturServer.Extensions
{
   public static class DbContextExtensions
   {

      /// <summary>
      /// DI for Mongo DB Connection
      /// </summary>
      /// <param name="services"></param>
      /// <param name="configuration"></param>
      public static void ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
      {
         services.Configure<Settings>(options =>
         {
            options.ConnectionString = configuration.GetSection("MongoConnection:ConnectionString").Value;
            options.Database = configuration.GetSection("MongoConnection:Database").Value;
         });
      }

      /// <summary>
      /// DI for Maria DB Connection
      /// </summary>
      /// <param name="services"></param>
      /// <param name="configuration"></param>
      public static void ConfigureMariaDb(this IServiceCollection services, IConfiguration configuration)
      {
         services.AddDbContext<MariaDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("DefaultConnection"),ServerVersion.FromString("10.2.13-mariadb"),
               b => b.MigrationsAssembly("AngularWebpackVisualStudio")));
      }
   }
}