using Otus.Highload.Models.Dtos;

namespace Otus.Highload.Middlewares;

/// <summary>
/// Exception middleware.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="next">Next middleware.</param>
    /// <param name="loggerFactory">Logger.</param>
    public ExceptionHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<ExceptionHandlingMiddleware>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Непредвиденная ошибка сервера");

            var errorResponse = new ErrorResponse
            {
                Message = "Непредвиденная ошибка сервера",
                Code = 500,
                RequestId = context.TraceIdentifier
            };

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var jsonResponse = System.Text.Json.JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}