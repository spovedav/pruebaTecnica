using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NUGET.SERILOG3_1;
using Serilog;
using System;

namespace CAPA.API.AUTH
{
    public class Program
    {
        public static void Main(string[] args)
        {

            SerilogConfiguration.ConfigureLogging();

            try
            {
                // Iniciar la aplicaci�n
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "La aplicaci�n fall� al iniciarse");
            }
            finally
            {
                Log.CloseAndFlush(); // Asegurarse de que los logs se guarden antes de cerrar
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
