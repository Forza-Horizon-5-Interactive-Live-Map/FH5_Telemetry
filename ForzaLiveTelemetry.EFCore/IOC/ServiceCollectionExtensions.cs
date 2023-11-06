using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForzaLiveTelemetry.EFCore.IOC;
public static class ServiceCollectionExtensions
{
    public static void AddLiveMapDb(this IServiceCollection services, ConfigurationManager configuration)
    {
        string connectionString = configuration.GetConnectionString("LiveMapSQL");
        services.AddDbContext<UserContext>(options =>
             options.UseSqlServer(
                connectionString == "DOCKER_CONNECTION_STRING" ? Environment.GetEnvironmentVariable("CONNECTION_STRING") : connectionString
                //x => x.MigrationsAssembly(typeof(UserContext).Assembly.FullName)
                ), ServiceLifetime.Scoped);
    }

    public static void ApplyMigration(this IServiceProvider services)
    {
        using (IServiceScope serviceScope = services.CreateScope())
        {
            UserContext? context = serviceScope.ServiceProvider.GetService<UserContext>();
            if (context != null)
            {
                if (context.Database.IsRelational())
                {
                    context?.Database.Migrate();
                }
                else
                {
                    context.Database.EnsureCreated();
                }
            }
        }
    }
}
