using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace WebApi;
public class Program
{
    public static void Main(string[] args)
    {
        Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        logger.Debug("Application Start");
        CreateHostBuilder(args).Build().Run();
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();

            }).ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            }).UseNLog();
}

