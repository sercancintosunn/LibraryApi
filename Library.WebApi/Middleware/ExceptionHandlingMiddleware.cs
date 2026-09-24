using System.Net;
using System.Text.Json;
using LibraryApi.Business.Exceptions;

namespace LibraryApi.WebApi.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            catch(Exception ex)
            {
                _logger.LogError(ex, "Beklenmeyen bir hata oluştu");
                await HandleExceptionAsync(context, ex);
            }

        }

        public static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, message) = exception switch
            {
                InvalidCredentialsException => (HttpStatusCode.Unauthorized, exception.Message),
                DuplicateEmailException => (HttpStatusCode.Conflict, exception.Message),
                BusinessRuleException => (HttpStatusCode.BadRequest, exception.Message),
                KeyNotFoundException => (HttpStatusCode.NotFound, exception.Message),
                UnauthorizedAccessException => (HttpStatusCode.Forbidden, exception.Message),
                _ => (HttpStatusCode.InternalServerError, "İşlem sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyin.")
            };

            var response = new
            {
                message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));


        }

    }
}
