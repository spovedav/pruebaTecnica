using CAPA.API.AUTH.Middleware;
using CAPA.APP.Interfaces.Respositorio;
using CAPA.APP.Interfaces.Servicios;
using CAPA.APP.Servicios;
using CAPA.APP.Utilities;
using CAPA.INFRE.EF;
using CAPA.INFRE.Respositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NUGET.SERILOG3_1;
using NUGET.TOOL.CORE._3_1.AES;
using Serilog;
using System.Collections.Generic;
using System.Data;

namespace CAPA.API.AUTH
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
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Api Auth",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    Description = "Ingrese su usuario y contraseña en formato Base64 (admin:password)"
                });

                // Requerir el esquema en todas las peticiones
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Basic"
                            }
                        },
                        new List<string>()
                    }
                });
            });

            UtilititesStartup.CargarDatosIniciales(Configuration);

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(UtilititesStartup.Cadena));

            #region SERVICOS
            services.AddScoped<IUsuarioServices, UsuarioServices>();
            services.AddScoped<IJwtServices, JwtServices>();
            #endregion

            #region REPOSITORIOS
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            #endregion

            #region INJECION DEPENDICIA NUGET
            services.AddSingleton<IAesEncryption>(_ => new AesEncryption(Configuration["Aes:Key"], Configuration["Aes:Iv"]));
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(optios =>
                {
                    optios.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Auth V1");
                    optios.RoutePrefix = string.Empty;
                });
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();
            //app.UseMiddleware<BasicAuthMiddleware>();
            app.UseAuthorization();
            app.UseGlobalExceptionLogging();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
