using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NUGET.SERILOG3_1;
using Serilog;
using System;

namespace CAPA.API.ADMIN
{
    public class Program
    {
        public static void Main(string[] args)
        {

            SerilogConfiguration.ConfigureLogging();

            try
            {
                // Iniciar la aplicación
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "La aplicación falló al iniciarse");
            }
            finally
            {
                Log.CloseAndFlush(); // Asegurarnos de que los logs se guardan antes de cerrar
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .UseSerilog();
                });
    }
}
