using Serilog;

namespace Monitoring;

public class MonitorService
{
    public static ILogger Log => Serilog.Log.Logger;

    static MonitorService()
    {
        Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Seq("http://seq:5341")
            .CreateLogger();
    }
}