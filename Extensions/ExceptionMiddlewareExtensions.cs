using System.Net;
using KinderKulturServer.Contracts;
using KinderKulturServer.CustomExceptionMiddleware;
using KinderKulturServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace KinderKulturServer.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        // /// <summary>
        // /// Exception Handling Config
        // /// </summary>
        // /// <param name="app"></param>
        // /// <param name="logger"></param>
        // public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        // {
        // 	app.UseExceptionHandler(
        // 		appError => appError.Run(async context =>
        // 		{
        // 			context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        // 			context.Response.ContentType = "application/json";

        // 			var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        // 			if (contextFeature != null)
        // 			{
        // 				logger.LogError($"Something went wrong: {contextFeature.Error}");

        // 				await context.Response.WriteAsync(new ErrorDetails()
        // 				{
        // 					StatusCode = context.Response.StatusCode,
        // 						Message = "Internal Server Error."
        // 				}.ToString());
        // 			}
        // 		})
        // 	);
        // }

        public static void ConfigureCustomExceptionHandler(this IApplicationBuilder app)
        {

            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}