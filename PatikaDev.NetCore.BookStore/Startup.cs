using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PatikaDev.NetCore.BookStore.DBOperations;
using PatikaDev.NetCore.BookStore.Middlewares;
using PatikaDev.NetCore.BookStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PatikaDev.NetCore.BookStore", Version = "v1" });
            });
            services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName:"BookStoreDB"));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton<ILoggerService, DBLogger>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PatikaDev.NetCore.BookStore v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("Middleware1 basladi");
            //    await next.Invoke();
            //    Console.WriteLine("Middleware1 sonlandi.");
            //});
            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("Middleware2 basladi");
            //    await next.Invoke();
            //    Console.WriteLine("Middleware2 sonlandi.");
            //});

            //app.UseFirst();

            //app.Use(async(context, next)=>
            //{
            //Console.WriteLine("Use Middleware tetiklendi");
            //await next.Invoke();
            //});

            //app.Map("/example", internalApp =>
            //internalApp.Run(async context =>
            //{
            //Console.WriteLine("/example middleware tetiklendi.");
            //await context.Response.WriteAsync("/example middleware tetiklendi.");
            //}));

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("Use Middleware tetiklendi2");
            //    await next.Invoke();
            //});

            //app.MapWhen(x=>x.Request.Method == "GET", internalApp =>
            //{
            //    internalApp.Run(async context =>
            //    {
            //        Console.WriteLine("Get istegi ile Middleware tetiklendi");
            //        await context.Response.WriteAsync("Get istegi ile Middleware tetiklendi");
            //    });
            //});

            //app.MapWhen(x => x.Request.Method == "PUT", internalApp =>
            //{
            //    internalApp.Run(async context =>
            //    {
            //        Console.WriteLine("Put istegi ile  Middleware tetiklendi");
            //        await context.Response.WriteAsync("Put istegi ile Middleware tetiklendi");
            //    });
            //});

            app.UseCustomExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
