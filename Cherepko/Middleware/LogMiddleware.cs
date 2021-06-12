using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Cherepko.Middleware
{
    public class LogMiddleware
    {
        RequestDelegate next;
        Microsoft.Extensions.Logging.ILogger<LogMiddleware> logger;
        public LogMiddleware(RequestDelegate Next,
        ILogger<LogMiddleware> Logger)
        {
            next = Next;
            logger = Logger;
        }
        public async Task Invoke(HttpContext context)
        {
            await next.Invoke(context);
            if (context.Response.StatusCode != StatusCodes.Status200OK)
            {
                var path = context.Request.Path + context.Request.QueryString;
                logger.LogInformation($"Request {path} returns status code {context.Response.StatusCode.ToString()}");
            }
        }
    }
}