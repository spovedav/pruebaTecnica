using CAPA.APP.Interfaces.Respositorio;
using CAPA.APP.Interfaces.Servicios;
using CAPA.APP.Servicios;
using CAPA.APP.Utilities;
using CAPA.DOMAIN.Static;
using CAPA.INFRE.EF;
using CAPA.INFRE.EF.Repositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NUGET.SERILOG3_1;
using NUGET.TOOL.CORE._3_1.AES;
using Serilog;
using System.Text;

namespace CAPA.API.ADMIN
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

            //services.AddSwaggerGen();

            UtilititesStartup.CargarDatosIniciales(Configuration);

            //services.AddScoped<IDbConnection>(_ => new SqlConnection(UtilititesStartup.Cadena));

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(UtilititesStartup.Cadena));

            #region SERVICOS
            services.AddScoped<IUsuarioServices, UsuarioServices>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IEmisorService, EmisorService>();
            services.AddScoped<ITransaccionService, TransaccionService>();
            services.AddScoped<IJwtServices, JwtServices>();
            #endregion

            #region REPOSITORIOS
            //services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IUsuarioRepositorio, INFRE.EF.Repositorio.UsuarioRepositorio>();
            services.AddScoped<IProductoRepositorio, INFRE.EF.Repositorio.ProductoRepositorio>();
            services.AddScoped<ITransaccionRepositorio, TransaccionRepositorio>();
            #endregion

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,// esto para que es
                        ValidateIssuerSigningKey = true,
                        ValidAudience = "mi-dominio",
                        ValidIssuer = "mi-dominio",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ParametrosApiConfi.KEY))
                    };
                });

            services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
            });

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
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "CAPA API ADMIN");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseAuthorization();
            app.UseGlobalExceptionLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
