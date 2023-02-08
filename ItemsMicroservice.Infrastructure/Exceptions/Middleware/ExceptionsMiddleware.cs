using ItemsMicroservice.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ItemsMicroservice.Infrastructure.Exceptions.Middleware;
internal sealed class ExceptionsMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionsMiddleware> _logger;
    private record Error(string Code, string Reason);

    public ExceptionsMiddleware(ILogger<ExceptionsMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            await HandleExceptionAsync(exception, context);
        }
    }

    private async Task HandleExceptionAsync(Exception exception, HttpContext context)
    {
        (var statusCode, var error) = exception switch
        {
            ItemsMicroserviceException => (StatusCodes.Status400BadRequest,
                new Error(exception
                    .GetType()
                    .Name
                    .Replace("_exception", string.Empty), exception.Message)),
            _ => (StatusCodes.Status500InternalServerError, new Error("Server error", exception.Message))
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }
}