using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NuGen.Dal;
using NuGen.Options.Start;
using NuGen.Services;
using NuGen.Services.Fsharp;
using NuGen.Services.Interfaces;

namespace NuGen
{
    static class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args).ConfigureServices((context, services) =>
            {
                services.Configure<StartOptions>(context.Configuration);
                services.AddDbContext<CacheDbContext>(builder => { builder.UseSqlite("Data Source=\"./cache.db\""); });
                services.AddScoped<IRandomGeneratorService, RandomGeneratorService>();
                services.AddScoped<IUniqCheckService, UniqCheckSimple>();
                services.AddScoped<IFileSystemService, FileSystemService>();
                services.AddScoped<IWriterService, FileWriterService>();
                services.AddScoped<IConsoleHelperService, ConsoleHelperService>();
                services.AddSingleton<IStateMonitoringService, StateMonitoringService>();
                services.AddHostedService<MainService>();
            }).ConfigureLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Error);
                builder.AddConsole();
            }).ConfigureAppConfiguration((_, configBuilder) =>
            {
                var mappings = new Dictionary<string, string>
                {
                    {"--from", nameof(StartOptions.From)},
                    {"-f", nameof(StartOptions.From)},
                    {"--to", nameof(StartOptions.To)},
                    {"-t", nameof(StartOptions.To)},
                    {"--prefix", nameof(StartOptions.Prefix)},
                    {"-p", nameof(StartOptions.Prefix)},
                    {"--output", nameof(StartOptions.FilePath)},
                    {"-o", nameof(StartOptions.FilePath)},
                    {"-digits", nameof(StartOptions.NumberOfDigits)},
                    {"-d", nameof(StartOptions.NumberOfDigits)},
                    {"-number-length", nameof(StartOptions.NumbersInOneFile)},
                    {"-n", nameof(StartOptions.NumbersInOneFile)},
                };
                configBuilder.AddCommandLine(args, mappings);
            }).RunConsoleAsync();
        }
    }
}