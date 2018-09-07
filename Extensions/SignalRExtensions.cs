using KinderKulturServer.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace KinderKulturServer.Extensions
{
   public static class SignalRExtenions
   {

      public static void ConfigureSignalR(this IApplicationBuilder app)
      {
         app.UseSignalR(routes =>
         {
            routes.MapHub<ChatHub>("/loopy");
         });
      }
   }
}