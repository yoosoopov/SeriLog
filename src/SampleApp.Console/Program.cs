using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Debugging;

namespace SampleApp.Console
{
    class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        
        static void Main(string[] args)
        {           
            SelfLog.Enable(System.Console.WriteLine);
            
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithCaller()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
            
            Log.Information("Hi there...");
            Log.Error("Error message sample {@Position}", new { Lat = 10, Long = 54 });
            
            Log.CloseAndFlush();
        }
    }
}
