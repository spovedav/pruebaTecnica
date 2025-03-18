using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUGET.SERILOG3_1
{
    public class SerilogConfiguration
    {
        public static void ConfigureLogging(string logFilePath = "logs/log-.txt")
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error() // Registra solo niveles de Information o mayores
                 .Enrich.FromLogContext()  // Incluir información del contexto (RequestId, etc.)
                 .Enrich.WithProperty("Application", "MyApp")  // Enriquecer logs con una propiedad adicional
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                //.WriteTo.Seq("http://localhost:5341")  // Enviar logs a un servidor de Seq
                .CreateLogger();
        }
    }
}
