using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
//using Microsoft.Extensions.Logging.EventLog;

namespace PlaylistManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // NLog: Setup the logger first to catch all errors
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("initialize Program.Main()");
                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                // Catch setup errors
                logger.Error(ex, "Error with BuildWebHost()");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                //.ConfigureLogging((hostingContext, logging) =>
                //{
                //    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                //    logging.AddEventLog(new EventLogSettings() { SourceName = "PlaylistManager" }); // Log to Application Event log
                //    logging.AddDebug();
                //})
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog()  // NLog: setup NLog for Dependency injection
                .Build();
    }
}
