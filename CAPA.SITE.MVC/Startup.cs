using CAPA.APP.Interfaces.Servicios;
using CAPA.APP.Servicios;
using CAPA.APP.Utilities;
using CAPA.DOMAIN.Static;
using CAPA.HTTP.Interfaces;
using CAPA.HTTP.Servicio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CAPA.SITE.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAuthentication("CookieAuth")
                        .AddCookie("CookieAuth", options =>
                        {
                            options.LoginPath = "/Auth/Index"; // Ruta para iniciar sesión
                            options.ExpireTimeSpan = TimeSpan.FromHours(1); // Sesión activa por 1 hora
                            options.SlidingExpiration = true; // Renovar la sesión automáticamente si está activa
                        });

            UtilititesStartup.CargarDatosInicialesSite(Configuration);

            #region CONFIGURACIONES
            services.AddHttpClient<PeticionesHTTP>()
                .ConfigureHttpClient(client => client.Timeout = TimeSpan.FromSeconds(ParametrosConfi.CONFIG_TIME_OUT_GOLBAL));
            #endregion

            #region SERVICOS APIS
            // POR SI LEEN ESTO, SE QUE SE PUEDE HACER ALGO COMO services.ImplementarServicios(); 
            services.AddScoped<IPeticionesHTTP, PeticionesHTTP>();
            services.AddScoped<IApiAdminUsuario, ApiAdminUsuario>();
            services.AddScoped<IApiAdminProducto, ApiAdminProducto>();
            services.AddScoped<IApiAdminEmisor, ApiAdminEmisor>();
            services.AddScoped<IApiAdminTransaccion, ApiAdminTransaccion>();
            services.AddScoped<IApiAuth, ApiAuth>();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Pages/page-500");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //NUEVO
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Dashboard}/{action=Index}/{id?}");
            });
        }
    }
}
