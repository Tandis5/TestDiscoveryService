using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TestDiscoveryService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    config
                        //.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                        //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                    logging.AddConsole();
                });
    }
}
