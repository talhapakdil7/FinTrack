using System.Net;
using System.Text.Json;

namespace FinTrack.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Hatayı terminal/log sistemine yazar
            _logger.LogError(ex,
     "Unhandled exception. Path: {Path}, Method: {Method}",
     context.Request.Path,
     context.Request.Method);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                success = false,
                message = "Beklenmeyen bir hata oluştu.",
                detail = ex.Message
            };

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}