using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        public static void ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            // services.AddSwaggerDocument(config =>
            // {
            //     config.PostProcess = document =>
            //     {
                    // document.Info.Version = "v1";
                    // document.Info.Title = "kinderkultur.ch API";
                    // document.Info.Description = "An advanced ASP.NET Core web API";
                    // document.Info.TermsOfService = "None";
                    // document.Info.Contact = new NSwag.OpenApiContact
                    // {
                    //     Name = "Linus Wieland",
                    //     Email = "vitocorleone77@gmail.com",
                    //     Url = "http://kinderkultur.ch"
                    // };
                    // document.Info.License = new NSwag.OpenApiLicense
                    // {
                    //     Name = "Use under LICX",
                    //     Url = "https://example.com/license"
                    // };
      //          };

                // config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT token"));
                // config.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT token",
                //     new SwaggerSecurityScheme
                //     {
                //         Type = SwaggerSecuritySchemeType.ApiKey,
                //         Name = "Authorization",
                //         Description = "Copy 'Bearer ' + valid JWT token into field",
                //         In = SwaggerSecurityApiKeyLocation.Header,
                //     }
                // ));
           // });
        }
    }
}