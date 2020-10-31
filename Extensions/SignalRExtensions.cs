using KinderKulturServer.SignalRHubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace KinderKulturServer.Extensions
{
    /// <summary>
    /// Signal R Config
    /// </summary>
    public static class SignalRExtensions
    {
        /// <summary>
        /// Configurate Signal R
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureSignalR(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/loopy");
            });
        }
    }
}