using Serilog;

namespace NUGET.SERILOG
{
    public class SerilogConfiguration
    {
        public static void ConfigureLogging(string logFilePath = "logs/log-.txt")
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
