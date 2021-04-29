using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NuGen.Options.Start;
using NuGen.Services;

namespace NuGen
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args).ConfigureServices((context, services) =>
            {
                services.Configure<StartOptions>(context.Configuration);
                services.AddHostedService<MainService>();
            }).ConfigureAppConfiguration((context, config) =>
            {
                var mappings = new Dictionary<string, string>
                {
                    {"--from", "From"},
                    {"-f", "From"},
                    {"--to", "To"},
                    {"-t", "To"}
                };
                config.AddCommandLine(args, mappings);
            }).RunConsoleAsync();
        }
    }
}
