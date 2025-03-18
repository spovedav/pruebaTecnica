using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace NUGET.SERILOG
{
    public class GlobalExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionLoggingMiddleware> _logger;

        public GlobalExceptionLoggingMiddleware(RequestDelegate next, ILogger<GlobalExceptionLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Registrar la excepción con detalles adicionales
                _logger.LogError(ex, "Se produjo una excepción no controlada durante la solicitud.");
                _logger.LogError("Request Path: {Path}, Method: {Method}, IP Address: {IP}",
                    context.Request.Path, context.Request.Method, context.Connection.RemoteIpAddress);

                // Retornar una respuesta controlada al cliente
                context.Response.StatusCode = 500; // Internal Server Error
                await context.Response.WriteAsync("Se produjo un error inesperado. Inténtelo de nuevo más tarde.");
            }
        }
    }
}
