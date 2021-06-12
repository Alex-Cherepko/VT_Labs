﻿using Cherepko.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Cherepko.Extensions
{
    public static class AppExtensions
    {
        public static IApplicationBuilder UseFileLogging(this IApplicationBuilder app)
        => app.UseMiddleware<LogMiddleware>();
    }
}