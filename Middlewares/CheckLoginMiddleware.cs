using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace BTVN11.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CheckLoginMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckLoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            //lấy đường dẫn url 
            var path = httpContext.Request.Path.ToString();
            //nếu path bát đầu  bằng chữ /Admin thì kiểm tra xem session đã tồn tại  chưa
            //nếu chưa tồn tại thì di chuyern đến trang login
            path = path.ToLower();
            if (path != "" && path.StartsWith("/admin"))
            {
                if (httpContext.Session.GetString("email") == null)
                {
                    //dat session de kiem duyet trang thai da dang nhap
                    //di chuyển đến trang login
                    httpContext.Response.Redirect("/Login");
                }
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CheckLoginMiddlewareExtensions
    {
        public static IApplicationBuilder UseCheckLoginMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckLoginMiddleware>();
        }
    }
}
