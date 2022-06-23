using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace MarsRover
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = AppStartup();

            var roverMovementsService = ActivatorUtilities.CreateInstance<RoverMovementService>(host.Services);
            Log.Information("Application Starting");
            roverMovementsService.Run();
            Log.Information("Application Completed");
        }

        static IHost AppStartup()
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();
            
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IRoverMovementService, RoverMovementService>();
                    services.AddTransient<GridSize>();
                    services.AddTransient<RoverPosition>();
                })
                .UseSerilog()
                .Build();

            return host;
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}
