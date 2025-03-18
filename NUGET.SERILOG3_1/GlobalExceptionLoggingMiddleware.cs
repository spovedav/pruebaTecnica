using CAPA.DOMAIN.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace NUGET.SERILOG3_1
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
                string guuid = Guid.NewGuid().ToString();

                //TODO: Con esto se puede registar cual error no controlado por la api, y aparte se le puso un código de siguimiento
                // SE PUEDE MEJORAR, ES SOLO PARA TENER MÁS EXPERIENCIA QUE AYER
                _logger.LogError(ex, guuid + "=> Se produjo una excepción no controlada durante la solicitud.");
                _logger.LogError(guuid + "=> Request Path: {Path}, Method: {Method}, IP Address: {IP}",
                    context.Request.Path, context.Request.Method, context.Connection.RemoteIpAddress);

                var errorResponse = new ResponseDto<object>();
                errorResponse.Mensaje = $"Error Grave  por favor póngase en contacto con el área de soporte, códigoSeguimiento: {guuid}";
                errorResponse.TieneError = true;
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                errorResponse.CodigoError = "SC_" + context.Response.StatusCode;
                context.Response.ContentType = "application/json";

                var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
