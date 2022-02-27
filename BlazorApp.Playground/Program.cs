using System.Diagnostics;
using BlazorApp.Playground.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Microsoft.AspNetCore.ResponseCompression;
using BlazorServerSignalRApp.Hubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<StateContainer>();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

builder.Services.AddHostedService<WorkerInBlazor>();


//builder.Services.AddDbContext<FlightsDbContext>(options =>
//    options.UseNpgsql("Server=127.0.0.1;Port=5432;Database=dbEfCore;User Id=postgres;Password=postgres;CommandTimeout=20;SearchPath=hello;")
//        .LogTo(StdOut)
//        .EnableSensitiveDataLogging(true), ServiceLifetime.Scoped);
builder.Services.AddScoped<FlightRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

//app.MapGet("/", () => "Hello World!");
app.Use(async (context, next) =>
{
    // ...
    Debug.WriteLine($"[dbg] pipeline: {context.Request.Path} next: type={next.Target?.GetType().Name} method={next.Method.Name}");
    await next(context);
});

app.MapHub<ChatHub>("/chathub");
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

void StdOut(string obj)
{
    Debug.WriteLine(obj);
}

public class WorkerInBlazor : BackgroundService
{
    private readonly StateContainer _container;

    public WorkerInBlazor(StateContainer container)
    {
        _container = container;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _container.RunAsync();
    }
}