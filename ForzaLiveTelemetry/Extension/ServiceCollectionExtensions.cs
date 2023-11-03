using ForzaLiveTelemetry.Domain.Entity;
using ForzaLiveTelemetry.Domain.Helper;
using ForzaLiveTelemetry.Domain.Setting;
using ForzaLiveTelemetry.EFCore;
using ForzaLiveTelemetry.Services;
using Microsoft.AspNetCore.Identity;

namespace ForzaLiveTelemetry.Extension;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSingleton(configuration.GetSection("Settings").Get<Settings>())
            .AddSingleton<MessagesService>()
            .AddSingleton<PlayersService>()
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
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole<Guid>>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;

            options.ClaimsIdentity.RoleClaimType = "Roles";
            options.ClaimsIdentity.UserIdClaimType = "Username";

            //Password requirement
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 4; //Determine le nombre de caract�re unnique minimum requis


            //Lockout si mdp fail 5 fois alors compte bloquer 60 min
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
            options.Lockout.AllowedForNewUsers = true;

            //User
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        })
        .AddDefaultTokenProviders()
        .AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<UserContext>();


        services.ConfigureApplicationCookie(options =>
        {
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };
        });

    }
}
