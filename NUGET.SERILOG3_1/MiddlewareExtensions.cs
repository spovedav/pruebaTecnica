using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUGET.SERILOG3_1
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionLoggingMiddleware>();
        }
    }
}
