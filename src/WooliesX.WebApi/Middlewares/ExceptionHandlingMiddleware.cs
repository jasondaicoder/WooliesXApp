using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WooliesX.WebApi.Models;

namespace WooliesX.WebApi.Middlewares
{
    /// <summary>
    /// Global exception handling in the middleware level, see below code how to add this middleware into asp.net core pipeline.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        private readonly IHostingEnvironment _env;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next">Instance of <see cref="RequestDelegate"/></param>
        /// <param name="logger">Instance of <see cref="ILogger{ExceptionHandlingMiddleware}"/></param>
        /// <param name="env">Instance of <see cref="IHostingEnvironment"/></param>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostingEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        /// <summary>
        /// Invoke the exception middleware
        /// </summary>
        /// <param name="context">Http context object</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception.ToString());

            var code = HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new ErrorResponse { Message = _env.IsDevelopment() ? exception.ToString() : " An error occurred during the request."});
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}