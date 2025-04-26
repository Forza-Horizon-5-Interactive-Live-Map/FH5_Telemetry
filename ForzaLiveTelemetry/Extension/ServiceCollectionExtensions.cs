using ForzaLiveTelemetry.Domain.Helper;
using ForzaLiveTelemetry.Domain.Setting;
using ForzaLiveTelemetry.EFCore;
using ForzaLiveTelemetry.Services;
using Microsoft.AspNetCore.Identity;

namespace ForzaLiveTelemetry.Extension;

public static class ServiceCollectionExtensions
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true,
                reloadOnChange: true)
            .AddEnvironmentVariables();
        
        builder.Services.AddSingleton(builder.Configuration.GetSection("Settings").Get<Settings>());

    }
    public static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<MessagesService>()
            .AddSingleton<TelemetryListener>()
            .AddSingleton<CarNamesService>()
            .AddSingleton<MapUpdatesService>()
            .AddSingleton<UserService>()
            .AddHostedService(provider => provider.GetRequiredService<MapUpdatesService>());

        services.AddControllers();
    }

    public static void ConfigureCors(this IServiceCollection services, ConfigurationManager configuration)
    {
        List<string> originsAllowed = configuration.GetSection("CallsOrigins").Get<List<string>>();
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                       .WithOrigins(originsAllowed.ToArray())
                       .WithMethods("PUT", "DELETE", "GET", "OPTIONS", "POST")
                       .AllowAnyHeader()
                       .AllowCredentials()
                       .Build();
            });
        });
    }

    public static TextLogger SetupLogger(this IServiceCollection services)
    {
        TextLogger logger = new();
        services.AddSingleton<ILogger>(logger);
        return logger;
    }
}
