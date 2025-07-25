using System.Net;
using System.Text.Json;

namespace LibraryAPI.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                FormatGlobalException(context, ex);
            }
        }

        private async Task FormatGlobalException(HttpContext contex, Exception ex)
        {
            var traceId = Guid.NewGuid().ToString();
            contex.Response.Headers.Add("X-Trace-Id", traceId);
            var (status, titlu) = MapException(ex);
            var problem = new
            {
                Status = status,
                Message = titlu,
                Details = "Adresativa la administrator!"
            };

            contex.Response.ContentType = "application/json";
            var json = JsonSerializer.Serialize(problem);
            await contex.Response.WriteAsync(json);
        }

        private (HttpStatusCode, string title) MapException(Exception ex)
        {
            switch (ex)
            {
                case KeyNotFoundException x: return (HttpStatusCode.NotFound, "Resource not Found!");
                case NullReferenceException x: return (HttpStatusCode.InternalServerError, "Something went wrong!");
                case UnauthorizedAccessException x: return (HttpStatusCode.Unauthorized, "You are not authorized to perform given action!");
                default:
                    return (HttpStatusCode.InternalServerError, "Something went wrong!");
            }

        }
    }
}
