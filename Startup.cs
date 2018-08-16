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
    public class Startup
    {

        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            // Logging
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            // Config Files
            Configuration = env.ConfigureConfiguration();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
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

            services.AddSingleton<IJwtFactory, JwtFactory>();

            // Register the ConfigurationBuilder instance of FacebookAuthSettings
            services.Configure<FacebookAuthSettings>(Configuration.GetSection(nameof(FacebookAuthSettings)));

            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.ConfigureAuthentication(Configuration, _signingKey);

            services.AddAutoMapper();

            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerManager logger)
        {

            app.UseAuthentication();

            app.UseCors("AllowAllOrigins");

            app.UseDefaultFiles();

            app.UseHttpsRedirection();
            
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.ConfigureCustomExceptionHandler();

            app.ConfigureSwagger();

            app.ConfigureRouting();
        }
    }
}