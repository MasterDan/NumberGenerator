using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NuGen.Dal;
using NuGen.Options.Start;
using NuGen.Services;
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
                services.AddDbContext<CacheDbContext>(builder =>
                {
                    builder.UseSqlite("Data Source=\"./cache.db\"");
                });
                services.AddScoped<IRandomGenerator, RandomGenerator>();
                services.AddScoped<IWriterService, FileWriterService>();
                services.AddScoped<IUniqCheckService, UniqCheckService>();
                services.AddHostedService<MainService>();
            }).ConfigureAppConfiguration((_, configBuilder) =>
            {
                var mappings = new Dictionary<string, string>
                {
                    {"--from", "From"},
                    {"-f", "From"},
                    {"--to", "To"},
                    {"-t", "To"},
                    {"--prefix","Prefix"},
                    {"-p","Prefix"},
                    {"--output","FilePath"},
                    {"-o","FilePath"}
                };
                configBuilder.AddCommandLine(args, mappings);
            }).RunConsoleAsync();
        }
    }
}
