using Sample.Business.Abstract;
using Sample.Common.Result;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;

namespace Sample.WebAPI.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, IAccessService accessService)
        {
            Stream originalBody = context.Response.Body;
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


                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;

                    await _next(context);
                    watch.Stop();
                    memStream.Position = 0;
                    string responseBody = new StreamReader(memStream).ReadToEnd();

                    if (context.Request.Path == "/api/Access/Login")
                    {
                        string? userId =null;
                        if (context.User.Identity.IsAuthenticated)
                        {
                            if (context.User.Identity is ClaimsIdentity identity)
                            {
                                userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                            }
                        }

                        if (userId != null || responseBody.IndexOf("accessToken") > -1)
                        {
                            accessService.AddLoginResponseTimeStamp(new DataAccess.Entities.UserLoginResponseTimeStamp
                            {
                                TimeStamp = watch.Elapsed.TotalSeconds,
                                UserId = !string.IsNullOrEmpty(userId) ? int.Parse(userId) : null
                            });
                        }

                    }
                    message += "\n[Response] " + context.Request.Method + " - " + context.Request.Path + " - Responsed " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds;
                    Debug.WriteLine(message);

                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                }
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
