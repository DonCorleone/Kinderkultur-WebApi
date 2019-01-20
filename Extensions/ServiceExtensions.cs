using KinderKulturServer.Contracts;
using KinderKulturServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace KinderKulturServer.Extensions
{
    /// <summary>
    /// Dependency Injection for Services
    /// </summary>
    public static class ServiceExtensions
    {

        /// <summary>
        /// DI Cors Policy
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        // /// <summary>
        // /// DI for Repositories
        // /// </summary>
        // /// <param name="services"></param>
        // public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        // {
        //     services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        // }

        /// <summary>
        /// DI for Cookies
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCookies(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        /// <summary>
        /// DI for API behavior
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureApiBehavior(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}