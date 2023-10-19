using ForzaLiveTelemetry.Domain.Helper;
using ForzaLiveTelemetry.EFCore.IOC;
using ForzaLiveTelemetry.Errors;
using ForzaLiveTelemetry.Extension;
using ForzaLiveTelemetry.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLiveMapDb(builder.Configuration);

builder.Services.AddSignalR();
builder.Services.ConfigureCors(builder.Configuration);

TextLogger logger = builder.Services.SetupLogger();

builder.Services.ConfigureIdentity();

WebApplication app = builder.Build();

app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.Services.ConfigureDatabase();
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

TelemetryListener? listener = app.Services.GetService<TelemetryListener>();
listener!.StartListener();
CarNamesService? carNamesService = app.Services.GetService<CarNamesService>();
await carNamesService!.LoadInfos();

app.Run();

public partial class Program
{
    protected Program()
    {
    }
}