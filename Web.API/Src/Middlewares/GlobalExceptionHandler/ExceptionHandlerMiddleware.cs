using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);                
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception: {Message}", e.Message); 
                var details = new ProblemDetails(){
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server Error"
                };            
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(details);
            }
        }
    }
}