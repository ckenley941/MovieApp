using MovieApp.Api.Common;
using System.Net;
using System.Text.Json;
using System.Web.Http.ExceptionHandling;

namespace MovieApp.Api.Infrastructure
{
    /// <summary>
    /// Custom Exception Handler middleware to intercept any exceptions in the class so that the user receives a generic server error message
    /// Eventually could add logging and other features to this to help developers troubleshoot exceptions
    /// </summary>
    public class AppExceptionHandler : ExceptionLogger
    {
        private readonly RequestDelegate _next;
        public AppExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
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
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            var stackTrace = String.Empty;
            string message;
            var exceptionType = exception.GetType();
            if (exceptionType == typeof(NotImplementedException))
            {
                status = HttpStatusCode.NotImplemented;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                status = HttpStatusCode.Unauthorized;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(KeyNotFoundException))
            {
                status = HttpStatusCode.Unauthorized;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            var exceptionResult = JsonSerializer.Serialize(new
            {
                error = message,
                stackTrace
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            //Future TODO - Log exceptionResult to ErrorTable in DB or CloudWatch to help with troubleshooting
            
            return context.Response.WriteAsync(AppConstants.Messages.InternalError);
        }
    }
}
