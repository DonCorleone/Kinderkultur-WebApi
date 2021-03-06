﻿// using FluentValidation.AspNetCore;
using KinderKulturServer.Contracts;
using KinderKulturServer.Data;
using KinderKulturServer.Extensions;
using KinderKulturServer.Handler;
using KinderKulturServer.Repositories;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AutoMapper;
using Microsoft.OpenApi.Models;

namespace KinderKulturServer
{
    /// <summary>
    /// .NET Core Boot Class
    /// </summary>
    public class Startup
    {
        // private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // ToDo: get this from somewhere secure
        // private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        // // Member var for Configuration 
        // public IConfigurationRoot Configuration { get; }

        // /// <summary>
        // /// Applications Startup Routine
        // /// </summary>
        // /// <param name="env"></param>
        // public Startup(IWebHostEnvironment env)
        // {
        //     // Logging
        //     LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

        //     // Config Files
        //     // Configuration = env.;
        // }

        // /// <summary>
        // /// This method gets called by the runtime. Use this method to add services to the container.
        // /// </summary>
        // /// <param name="services"></param>
        // public void ConfigureServices(IServiceCollection services)
        // {
        //     // Add framework services.
        //     services.ConfigureCookies();

        //     // .NET Core WebApi functionality
        //     services.AddMvc()
        //         .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);  // ToDo NetCore 2.2

        //     services.ConfigureApiBehavior();

        //     // ToDo : ev. wieder rein fuer Docker
        //     // services.AddHttpsRedirection(options =>
        //     // {
        //     //     options.HttpsPort = 5001;
        //     // }); 



        //     // JWT Singleton 
        //     services.AddSingleton<IJwtFactory, JwtFactory>();

        //     // Register the ConfigurationBuilder instance of FacebookAuthSettings
        //     services.Configure<FacebookAuthSettings>(Configuration.GetSection(nameof(FacebookAuthSettings)));

        //     // Authentication
        //     services.ConfigureAuthentication(Configuration, _signingKey);

        //     // DI for Signal R
        //     services.AddSignalR();

        //     // MVC Builder
        //     services
        //         .AddMvc()
        //         .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
        // }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // /// <summary>
        // /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // /// </summary>
        // /// <param name="app"></param>
        // /// <param name="env"></param>
        // /// <param name="logger"></param>
        // public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        // {

        //     // Authentication
        //     app.UseAuthentication();


        //     // Current Path
        //     app.UseDefaultFiles();


        //     // Cookies
        //     app.UseCookiePolicy();

        //     // // Cross Domain Request Part 2
        //     // app.UseCors("CorsPolicy");


        //     app.UseCors(MyAllowSpecificOrigins); 

        //     // Debug Info for Exceptions
        //     if (env.EnvironmentName == "Development")
        //         app.UseDeveloperExceptionPage();

        //     // Custom Exceptions
        //     app.ConfigureCustomExceptionHandler();



        //     // Signal R
        //     app.ConfigureSignalR();

        //     // Adds Routing
        //     app.ConfigureRouting();
        // }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder
                        .WithOrigins("https://localhost:8080")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.ConfigureMongoDb(Configuration);

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<MongoDBContext>();

            services.ConfigureMariaDb(Configuration);

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            services.AddTransient<IImageHandler, ImageHandler>();
            services.AddTransient<ImageWriter.Interface.IImageWriter,
                                  ImageWriter.Classes.ImageWriter>();

            services.AddApiVersioning();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors(MyAllowSpecificOrigins); 

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
   }
}