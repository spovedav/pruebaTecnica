using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;
using CAPA.DOMAIN.DTOs;
using System.Text.Json;
using CAPA.APP.Interfaces.Servicios;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Serilog;

namespace CAPA.API.AUTH.Middleware
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BasicAuthMiddleware> _logger;
        private readonly IUsuarioServices service;
        public BasicAuthMiddleware(RequestDelegate next, ILogger<BasicAuthMiddleware> _logger, IUsuarioServices service)
        {
            _next = next;
            this._logger = _logger;
            this.service = service;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {

                var errorResponse = new ResponseDto<object>();

                context.Response.ContentType = "application/json";

                if (!context.Request.Headers.ContainsKey("Authorization"))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    ValidFallido(ref errorResponse, "Falta el encabezado de autorización.", context.Response.StatusCode);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                    return;
                }

                var authHeader = context.Request.Headers["Authorization"].ToString();
                if (!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    ValidFallido(ref errorResponse, "Formato de encabezado de autorización no válido.", context.Response.StatusCode);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                    return;
                }

                var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
                var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
                var parts = decodedCredentials.Split(':', 2);

                string? UserName = parts?.FirstOrDefault();
                string? PassWord = parts?.FirstOrDefault();

                if (parts.Length != 2 || !ValidateUser(UserName, PassWord))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    ValidFallido(ref errorResponse, "Nombre de usuario o contraseña no válidos.", context.Response.StatusCode);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                    return;
                }

                var resultaServicio = service.ValidCredenciales(UserName, PassWord);

                if (resultaServicio != null || resultaServicio.Data == false)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    ValidFallido(ref errorResponse, "Nombre de usuario o contraseña no válidos.", context.Response.StatusCode);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                    return;
                }

                context.Items["Username"] = UserName;
                context.Items["Password"] = PassWord;
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Error",ex);
                Console.WriteLine($"Error en Middleware: {ex.Message}");
            }
            await _next(context);
        }

        private void ValidFallido(ref ResponseDto<object> response, string msg, int StatusCode)
        {
            response.TieneError = true;
            response.Mensaje = msg;
            response.CodigoError = "SC_" + StatusCode;
        }

        private bool ValidateUser(string username, string password)
        {
            // Credenciales de prueba (mejor usar configuración o BD)
            return username == "admin" && password == "password123";
        }
    }
}
