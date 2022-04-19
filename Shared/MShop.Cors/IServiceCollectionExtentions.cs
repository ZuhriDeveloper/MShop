using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace MShop.Cors
{
    public static class IServiceCollectionExtentions
    {
        public static IServiceCollection AddCatsCors(this IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var corsString = configuration.GetSection("CorsString").Value;
            var origins = corsString.Split(new[] { ',', ';' });

#if DEBUG
            Console.WriteLine("Origins:");
            foreach (var o in origins)
            {
                Console.WriteLine("   " + o);
            }
#endif

            return services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins(origins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }
    }
}