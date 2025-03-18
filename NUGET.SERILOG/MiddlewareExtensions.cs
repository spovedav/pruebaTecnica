using Microsoft.AspNetCore.Builder;

namespace NUGET.SERILOG
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionLoggingMiddleware>();
        }
    }
}
