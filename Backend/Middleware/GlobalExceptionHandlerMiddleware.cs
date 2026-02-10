using System.Net;
using System.Text.Json;
using ProductManagement.Exceptions;
using ProductManagement.Models;
using Serilog;

namespace ProductManagement.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;

        public GlobalExceptionHandlerMiddleware(
            RequestDelegate next,
            IHostEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var correlationId = Guid.NewGuid().ToString();

            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string error = "InternalServerError";
            string message = "An unexpected error occurred while processing your request";
            string? details = null;

            switch (exception)
            {
                case NotFoundException notFoundEx:
                    statusCode = HttpStatusCode.NotFound;
                    error = "NotFound";
                    message = notFoundEx.Message;
                    
                    Log.Warning(
                        "Resource not found | CorrelationId: {CorrelationId} | Path: {Path} | Message: {Message}",
                        correlationId,
                        context.Request.Path,
                        message);
                    break;

                case BadRequestException badRequestEx:
                    statusCode = HttpStatusCode.BadRequest;
                    error = "BadRequest";
                    message = badRequestEx.Message;
                    
                    Log.Warning(
                        "Bad request | CorrelationId: {CorrelationId} | Path: {Path} | Parameter: {Parameter} | Message: {Message}",
                        correlationId,
                        context.Request.Path,
                        badRequestEx.ParameterName,
                        message);
                    break;

                case ConflictException conflictEx:
                    statusCode = HttpStatusCode.Conflict;
                    error = "Conflict";
                    message = conflictEx.Message;
                    
                    Log.Warning(
                        "Conflict | CorrelationId: {CorrelationId} | Path: {Path} | Message: {Message}",
                        correlationId,
                        context.Request.Path,
                        message);
                    break;

                case InvalidOperationException invalidOpEx:
                    statusCode = HttpStatusCode.BadRequest;
                    error = "InvalidOperation";
                    message = invalidOpEx.Message;
                    
                    Log.Warning(
                        "Invalid operation | CorrelationId: {CorrelationId} | Path: {Path} | Message: {Message}",
                        correlationId,
                        context.Request.Path,
                        message);
                    break;

                case ArgumentException argEx:
                    statusCode = HttpStatusCode.BadRequest;
                    error = "InvalidArgument";
                    message = argEx.Message;
                    
                    Log.Warning(
                        "Invalid argument | CorrelationId: {CorrelationId} | Path: {Path} | Parameter: {Parameter} | Message: {Message}",
                        correlationId,
                        context.Request.Path,
                        argEx.ParamName,
                        message);
                    break;

                default:
                    Log.Error(
                        exception,
                        "💥 UNHANDLED EXCEPTION | CorrelationId: {CorrelationId} | Path: {Path} | Method: {Method} | User: {User}",
                        correlationId,
                        context.Request.Path,
                        context.Request.Method,
                        context.User.Identity?.Name ?? "Anonymous");
                    
                    message = "An internal server error occurred. Please contact support with the correlation ID.";
                    break;
            }

            if (_environment.IsDevelopment())
            {
                details = exception.ToString(); // Full stack trace
            }

            var errorResponse = new ErrorResponse(
                statusCode: (int)statusCode,
                error: error,
                message: message,
                details: details,
                path: context.Request.Path,
                correlationId: correlationId
            );

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var json = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            await context.Response.WriteAsync(json);
        }
    }
}