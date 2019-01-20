using Microsoft.AspNetCore.Builder;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.Processors.Security;

namespace KinderKulturServer.Extensions
{
    /// <summary>
    /// Swagger Config
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Configurate Swagger UI
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwaggerUi3WithApiExplorer(settings =>
            {
                settings.DocExpansion = "list";

                settings.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "kinderkultur.ch API";
                    document.Info.Description = "An advanced ASP.NET Core web API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name = "Linus Wieland",
                        Email = "vitocorleone77@gmail.com",
                        Url = "http://kinderkultur.ch"
                    };
                    document.Info.License = new NSwag.SwaggerLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };

                settings.GeneratorSettings.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT token"));

                settings.GeneratorSettings.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT token",
                    new SwaggerSecurityScheme
                    {
                        Type = SwaggerSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        Description = "Copy 'Bearer ' + valid JWT token into field",
                        In = SwaggerSecurityApiKeyLocation.Header,
                    }
                ));
            });
        }
    }
}