using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForzaLiveTelemetry.EFCore.IOC;
public static class ServiceCollectionExtensions
{
    public static void AddDbContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        
        bool useInMemory = Convert.ToBoolean(configuration["UseInMemory"]);
        string connectionString = configuration.GetConnectionString("LiveMapSQL")
                                  ?? configuration["CONNECTION_STRING"]
                                  ?? throw new ArgumentNullException("CONNECTION_STRING");
        
        services.AddDbContext<UserContext>(options =>
        {
            if (useInMemory)
            {
                options.UseInMemoryDatabase("LiveMap");
            }
            else
            {
                options.UseNpgsql(
                    connectionString, x => x.MigrationsAssembly(typeof(UserContext).Assembly.FullName)
                );
            }
        }, ServiceLifetime.Singleton);
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
