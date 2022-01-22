using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PatikaDev.NetCore.BookStore.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.Middlewares
{
    
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;
        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)//Kendinden sonraki middleware aktif edebilmesi icin RequestDelegate den faydalanir
        {
            _next = next;
            _loggerService = loggerService;
        }
        public async Task InvokeAsync(HttpContext context)//async ve await metotlarin geri dönüs tipi Task olur.
        {
            var watch = Stopwatch.StartNew();//zamani hesaplamak icin sayaci baslattik
            try
            {
                string message = "[Request] Http " + context.Request.Method + " - " + context.Request.Path + " - " + DateTime.Now;
                _loggerService.Write(message);
                await _next(context);//kendinden sonraki middleware cagirdi _next.Invoke(context) ile ayni islevde
                watch.Stop();
                message = "[Response] Http " + context.Request.Method + " - " + context.Request.Path + " - " + context.Response.StatusCode + " - response time: " + watch.Elapsed.TotalMilliseconds + "ms";
                _loggerService.Write(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleExeption(context, ex, watch);
            }
        }
        private Task HandleExeption(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json"; //genelde standart olarak json olur
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;//500 hata kodu doner
            
            string message = "[Error] Http " + context.Request.Method + " - " + context.Response.StatusCode + " - Error Message : " + ex.Message + " - response time: " + watch.Elapsed.TotalMilliseconds + "ms";
            _loggerService.Write(message);

            var result = JsonConvert.SerializeObject(new {error = ex.Message},Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }
    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)//app. ile cagrilabilmesi icin app builder yazdik.
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
