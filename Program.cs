using ForzaDynamicMapApi.Errors;
using ForzaDynamicMapApi.Helper;
using ForzaDynamicMapApi.Services;
using ForzaDynamicMapApi.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSignalR();
AddCORS();


builder.Services.AddSingleton(builder.Configuration.GetSection("Settings").Get<Settings>());

builder.Services.AddSingleton<MessagesService>();
builder.Services.AddSingleton<PlayersService>();
builder.Services.AddSingleton<TelemetryListener>();
builder.Services.AddSingleton<CarNamesService>();
builder.Services.AddSingleton<MapUpdatesService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<MapUpdatesService>());

var logger = new TextLogger();
builder.Services.AddSingleton<ILogger>(logger);

var app = builder.Build();

app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.EnablePersistAuthorization();
        s.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        s.DisplayRequestDuration();
        s.EnableFilter();
        s.EnableTryItOutByDefault();
    });

}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();

app.UseAuthorization();
app.MapControllers();
app.MapHub<MapUpdatesHub>("/mapUpdatesHub");


var listener = app.Services.GetService<TelemetryListener>();
listener!.StartListener();
var carNamesService = app.Services.GetService<CarNamesService>();
await carNamesService!.LoadInfos();

app.Run();


void AddCORS()
{
    List<string> originsAllowed = builder.Configuration.GetSection("CallsOrigins").Get<List<string>>();
    builder.Services.AddCors(o =>
    {
        o.AddDefaultPolicy(builder =>
        {
            builder.WithOrigins(originsAllowed.ToArray())
                .AllowAnyHeader()
                .WithMethods("PUT", "DELETE", "GET", "OPTIONS", "POST")
                .AllowCredentials()
                .WithExposedHeaders("content-disposition")
                .Build();
        });
    });
}
