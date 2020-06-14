using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using BTVN11.Models;
using MimeKit;
using MailKit.Net.Smtp;
//using System.Net.Mail;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace BTVN11.Controllers
{
    
    public class HomeController : Controller
    {
        private DataContext db = new DataContext();
        private IHostingEnvironment _env;

        public IActionResult Index()
        {
            

        //    //load template;
        //    var message = new MimeMessage();
        //    message.From.Add(new MailboxAddress("cát đậu", "dauthithuycat@gmail.com"));
        //    message.To.Add(new MailboxAddress("thùy cát", "khanghoa12388@gmail.com"));
        //    message.Subject = "thông báo lương";
        //    /*message.Body = *//*messageBody;*/
        //    message.Body = new TextPart("html")
        //    {
        //        Text = "hello my mail <br> xuoongs dong"
        //    };
        //    using (var client = new SmtpClient())
        //    {
        //        client.Connect("smtp.gmail.com", 587, false);
        //        client.Authenticate("dauthithuycat@gmail.com", "C26061997");
        //        client.Send(message);
        //        client.Disconnect(true);
        //    }

          return View("Index");

       }
        [HttpGet]
        public JsonResult loadData()
        {
            List<users> record = db.users.OrderBy(tbl=>tbl.id).ToList();
            List<EmployeeModel> record2 = new List<EmployeeModel>();
            record2.Add(new EmployeeModel()
            {
                ID = 1,
                Name="dau thuy cat",
                Salary=30000,
                stastus=false

            }) ;

            record2.Add(new EmployeeModel()
            {
                ID = 1,
                Name = "Nguyen van B",
                Salary = 20000,
                stastus = true

            });

            record2.Add(new EmployeeModel()
            {
                ID = 1,
                Name = "Nguyen Van C",
                Salary = 35000,
                stastus = true

            });

            record2.Add(new EmployeeModel()
            {
                ID = 1,
                Name = "Marry",
                Salary = 10000,
                stastus = false

            });
            return Json(record);

        }

        [HttpGet]
        public JsonResult getById(int id)
        {
            users us = db.users.Where(x => x.id == id).FirstOrDefault();
            return Json(us);
        }
         [HttpPost]
         public JsonResult AddUser(users User)
        {
            DataContext db = new DataContext();
            users record = new users();
            record.name = User.name;
            record.email = User.email;
            record.password = getHash(User.password);
            db.users.Add(record);
            db.SaveChanges();
            return Json(record);

        }
        //ham ma hoa password
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