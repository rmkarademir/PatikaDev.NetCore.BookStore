using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.Middlewares
{
    public class FirstMiddleware
    {
        private readonly RequestDelegate _next;
        public FirstMiddleware(RequestDelegate next)//Kendinden sonraki middleware aktif edebilmesi icin RequestDelegate den faydalanir
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)//async ve await metotlarin geri dönüs tipi Task olur.
        {
            Console.WriteLine("Ilk middleware basladi");
            await _next.Invoke(context);//kendinden sonraki middleware cagirdi
            Console.WriteLine("Ilk middleware sonlandi");
        }
    }
    public static class FirstMiddlewareExtension
    {
        public static IApplicationBuilder UseFirst(this IApplicationBuilder builder)//app. ile cagrilabilmesi icin app builder yazdik.
        {
            return builder.UseMiddleware<FirstMiddleware>();
        } 

    }



}
