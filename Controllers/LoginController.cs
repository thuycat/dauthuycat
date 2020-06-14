using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Security.Cryptography;
using System.Text;
using BTVN11.Models;
using Microsoft.AspNetCore.Http;

namespace BTVN11.Controllers
{
    public class LoginController : Controller
    {
        //đối tượng thao tác csdl
        private DataContext db = new DataContext();
        public IActionResult Index()
        {
            return View();
        }
      [HttpPost,ValidateAntiForgeryToken]
      public IActionResult LoginPost()
        {
            string _email = Request.Form["email"];
            string _password = Request.Form["password"];
            //mã hóa password
            _password = getHash(_password);
            //só sánh thông tin với csdl
            var record = db.users.Where(tbl => tbl.email == _email && tbl.password == _password).FirstOrDefault();
            if (record != null)
            {
                //dang nhạp thành công
                //set biến session để sử dụng kiểm tra các trang admin
               //using Microsoft.AspNetCore.Http;
                HttpContext.Session.SetString("email", _email);
                return Redirect("/Admin/Home?email=");
            }
            return Redirect("/Login");
        }
        //hàm mã hóa từ text sang chuỗi SHA256
            private string getHash(string text)
            {
                using (var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                    return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }
            }
    }
}