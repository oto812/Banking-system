using System.Net;
using System.Text.Json;
using BankingSystem.Application.Common;
using Domain.CustomExceptions;

namespace BankingSystem.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionHandlingMiddleware(RequestDelegate next, IHostEnvironment env,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        var statusCode = HttpStatusCode.InternalServerError;
        var message = "An unexpected error occurred.";
        IDictionary<string, string[]>? validationErrors = null;

        switch (ex)
        {
            case BaseException baseEx:
                statusCode = baseEx.StatusCode;
                message = baseEx.Message;
                break;
            
            case FluentValidation.ValidationException valEx:
                statusCode = HttpStatusCode.BadRequest;
                message = "One or more validation errors occurred.";
                validationErrors = valEx.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
                break;
        }
        context.Response.StatusCode = (int)statusCode;
        

        

        var response = new ErrorResponse
        {
            StatusCode = context.Response.StatusCode,
            Message = message,
            Errors = validationErrors,
            Details = _env.IsDevelopment() ? ex.StackTrace : null
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}

