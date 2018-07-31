using KinderKulturServer.Data;
using KinderKulturServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KinderKulturServer.Extensions
{
   public static class DbContextExtensions
   {

      public static void ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
      {
         services.Configure<Settings>(options =>
         {
            options.ConnectionString = configuration.GetSection("MongoConnection:ConnectionString").Value;
            options.Database = configuration.GetSection("MongoConnection:Database").Value;
         });
      }

      public static void ConfigureMariaDb(this IServiceCollection services, IConfiguration configuration)
      {
         services.AddDbContext<MariaDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
               b => b.MigrationsAssembly("AngularWebpackVisualStudio")));
      }
   }
}