using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using KinderKulturServer.Auth;
using KinderKulturServer.Contracts;
using KinderKulturServer.Data;
using KinderKulturServer.Extensions;
using KinderKulturServer.Handler;
using KinderKulturServer.Helpers;
using KinderKulturServer.Models;
using KinderKulturServer.Models.Entities;
using KinderKulturServer.Repositories;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NJsonSchema;
using NLog;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.Processors.Security;

namespace KinderKulturServer
{
    /// <summary>
    /// .NET Core Boot Class
    /// </summary>
    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // ToDo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        // Member var for Configuration 
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// Applications Startup Routine
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            // Logging
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            // Config Files
            Configuration = env.ConfigureConfiguration();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add service and create Policy with options 
            services.ConfigureCors();

            // Add framework services.
            services.ConfigureCookies();

            // .NET Core WebApi functionality
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.ConfigureApiBehavior();

            // ToDo : ev. wieder rein fuer Docker
            // services.AddHttpsRedirection(options =>
            // {
            //     options.HttpsPort = 5001;
            // }); 

            // API Versioning 
            services.AddApiVersioning();

            services.AddSingleton<ILoggerManager, LoggerManager>();

            // Register MariaDb Context.
            services.ConfigureMariaDb(Configuration);

            // MongoDB Connection Information
            services.ConfigureMongoDb(Configuration);

            // Register MongoDB Context
            services.AddScoped<MongoDBContext>();

            // Dependency Injection MongoDb Repo
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            services.AddTransient<IImageHandler, ImageHandler>();
            services.AddTransient<ImageWriter.Interface.IImageWriter,
                                  ImageWriter.Classes.ImageWriter>();

            // JWT Singleton 
            services.AddSingleton<IJwtFactory, JwtFactory>();

            // Register the ConfigurationBuilder instance of FacebookAuthSettings
            services.Configure<FacebookAuthSettings>(Configuration.GetSection(nameof(FacebookAuthSettings)));

            // HTTP Context
            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            // Authentication
            services.ConfigureAuthentication(Configuration, _signingKey);

            // DI for Automapper
            services.AddAutoMapper();

            // DI for Signal R
            services.AddSignalR();

            // MVC Builder
            services
                .AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerManager logger)
        {

            // Authentication
            app.UseAuthentication();

            // Cross Domain Requests
            app.UseCors("AllowAllOrigins");

            // Current Path
            app.UseDefaultFiles();

            // Force HTTPS
            app.UseHttpsRedirection();

            // Static File Serve
            app.UseStaticFiles();

            // Cookies
            app.UseCookiePolicy();

            // Cross Domain Request Part 2
            app.UseCors("CorsPolicy");

            // Debug Info for Exceptions
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            // Custom Exceptions
            app.ConfigureCustomExceptionHandler();

            // Swagger
            app.ConfigureSwagger();

            // Signal R
            app.ConfigureSignalR();

            // Adds Routing
            app.ConfigureRouting();
        }
    }
}