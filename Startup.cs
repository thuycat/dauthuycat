using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

//khai báo sử dụng class trong thư muc middlewares
using BTVN11.Middlewares;

namespace BTVN11
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //app sử dụng session
            services.AddSession();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            //sử dụng session
            app.UseSession();
            //sử dụng middleware checkLoginMiddlewear.cs
            app.UseMiddleware<CheckLoginMiddleware>();
            //sử dụng thư mục WWWroot
            app.UseStaticFiles();
            //sử dụng MVc
            app.UseMvc(route => { route.MapRoute(name: "Default", template: "{Controller=Home}/{Action=Index}/{id?}"); });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
