using Sample.Common.Result;
using System.Diagnostics;
using System.Net;

namespace Sample.WebAPI.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            string message = "";
            if (context.Request.Method == "Get")
            {
                message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path + "\n";
            }
            else
            {
                message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
                //message += "\nBody=" + context.Request.BodyReader;
            }
            if (context.Request.QueryString.HasValue)
            {
                message += "\nQuery= " + context.Request.QueryString.Value;
            }

            try
            {
                await _next.Invoke(context);
                watch.Stop();
                message += "\n[Response] " + context.Request.Method + " - " + context.Request.Path + " - Responsed " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds;
                Debug.WriteLine(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleExceptionControl(context, ex, watch);
            }
        }

        private async Task HandleExceptionControl(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var message = "\n[Error]" + context.Request.Method + " - " + context.Request.Path + " - Responsed " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + "\n Error: " + ex.Message + "\n" + ex.StackTrace;
            ServiceResult serviceResult = new ServiceResult(false, ex.Message);
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(serviceResult, Newtonsoft.Json.Formatting.None);
            await context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
