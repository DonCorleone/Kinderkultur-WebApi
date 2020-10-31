using Microsoft.AspNetCore.Builder;

namespace KinderKulturServer.Extensions
{
    public static class ApplicationExtensions
    {

        /// <summary>
        /// Adds a route to the Microsoft.AspNetCore.Routing.IRouteBuilder with the specified name and template.
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureRouting(this IApplicationBuilder app)
        {

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
              //  endpoints.MapControllerRoute(
              //      "default", "{controller=Home}/{action=Index}/{id?}");            
            });
        }
    }
}