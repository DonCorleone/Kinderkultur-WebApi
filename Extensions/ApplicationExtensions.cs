using Microsoft.AspNetCore.Builder;

namespace KinderKulturServer.Extensions
{
   public static class ApplicationExtensions
   {
      public static void ConfigureRouting(this IApplicationBuilder app)
      {
         app.UseMvc(routes =>
         {
            routes.MapRoute(
               name: "default",
               template: "{controller=Home}/{action=Index}/{id?}");
         });
      }
   }
}