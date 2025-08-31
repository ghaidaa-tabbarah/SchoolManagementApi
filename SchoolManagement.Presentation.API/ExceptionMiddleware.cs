using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Domain.Base;

namespace SchoolManagement.Presentation.API;

public class ExceptionMiddleware
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (BusinessException ex)
        {
            _logger.LogError($"ValidationException: {ex}");
            await HandleBusinessExceptionAsync(httpContext, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went Wrong 0oO" +
                             "\nMessage: {ErrorMessage}" +
                             "\nStackTrace: {ErrorStackTrace}", ex.Message, ex.StackTrace);

            var excepLoop = ex;
            var innerExceptionCounter = 1;
            while (excepLoop.InnerException != null)
            {
                _logger.LogError("InnerException[{innerExceptionCounter}] Message: {ErrorMessage}",
                    innerExceptionCounter, excepLoop.Message);
                _logger.LogError("InnerException[{innerExceptionCounter}] StackTrace: {ErrorStackTrace}",
                    innerExceptionCounter, ex.StackTrace);
                excepLoop = excepLoop.InnerException;
                innerExceptionCounter++;
            }

            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static async Task HandleBusinessExceptionAsync(HttpContext context, BusinessException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(
                new BadRequestObjectResult(exception.Message)
            )
        );
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        if (_env.IsProduction())
            await context.Response.WriteAsync(
                JsonSerializer.Serialize(
                    new BadRequestObjectResult("internal server error")
                )
            );
        else
            await context.Response.WriteAsync(
                JsonSerializer.Serialize(
                    new ExceptionResponse
                    {
                        Status = context.Response.StatusCode,
                        ExceptionMessage = exception.Message,
                        InnerException = exception.InnerException?.ToString(),
                        StackTrace = exception.StackTrace
                    }
                )
            );
    }

    private class ExceptionResponse
    {
        public int Status { get; set; }
        public string ExceptionMessage { get; set; } = string.Empty;
        public string? InnerException { get; set; }
        public string? StackTrace { get; set; }
    }
}