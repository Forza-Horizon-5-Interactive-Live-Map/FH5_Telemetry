using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForzaLiveTelemetry.EFCore.IOC;
public static class ServiceCollectionExtensions
{
    public static void AddLiveMapDb(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<UserContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("LiveMapSQL"),
                x => x.MigrationsAssembly(typeof(UserContext).Assembly.FullName)));
    }

    public static void ConfigureDatabase(this IServiceProvider services)
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
