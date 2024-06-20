using System.Net;
using System.Text.Json;
using Services.Exceptions;

namespace Controller.Utils;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = HttpStatusCode.InternalServerError;
        string message = exception.Message;

        if (exception is NotFoundException)
        {
            statusCode = HttpStatusCode.NotFound;
        }else if (exception is BadRequestException)
        {
            statusCode = HttpStatusCode.BadRequest;
        }

        context.Response.StatusCode = (int)statusCode;

        var response = new { message };
        var json = JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(json);
    }
}