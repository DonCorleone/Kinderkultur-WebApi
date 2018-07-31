using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace KinderKulturServer.Extensions
{
    public static class HostingEnvironmentExtensions
    {
         public static IConfigurationRoot ConfigureConfiguration(this IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional : true, reloadOnChange : true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional : true)
                .AddEnvironmentVariables();
            return builder.Build();
        }
    }
}