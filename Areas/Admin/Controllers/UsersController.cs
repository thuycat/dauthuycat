using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using BTVN11.Models;
using X.PagedList;

//ma hoa pasword
using System.Security.Cryptography;
//dùng để sử dụng cho UTF8
using System.Text;
using System.Collections;
//namespace project_news.Areas.Admin.Controllers
namespace BTVN11.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private DataContext db = new DataContext();
        public IActionResult List(int? page)
        {
            //khai báo so
            //quy định số bản ghi trên 1 trang
            int pageSize = 10;
            //kiểm tra nếu có biến page truyền vào thig lấy nó
            //còn không thì lất giá trị 1
            int pageNumber = page.HasValue ? Convert.ToInt32(page) : 1;
            //lấy các bản ghi, sap xếp theo id giảm dần

            var list_record = db.users.OrderByDescending(tbl => tbl.id).ToList();
            //SortedList ds = new SortedList();
            //foreach (var item in list_record)
            //{
            //    ds.Add(item, item.name);
            //}
           return View("List", list_record.ToPagedList(pageNumber, pageSize));
            //return Content("ok");
        }
        public IActionResult Edit()
        {
            //lay bien id truyen tu url
            int _id = int.Parse(Request.Query["id"]);
            //khai bao bien de chi action cua form
            ViewBag.FormAction = "/Admin/Users/EditPost?id=" + _id;
            //lay mot ban ghi
            var record = db.users.Where(tbl => tbl.id == _id).FirstOrDefault();
            return View("AddEditUser", record);
        }
        //khi an vao nut submit(chuc nang edit)
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditPost()
        {
            int _id = int.Parse(Request.Query["id"]);
            //lay cac bien  form control
            string _name = Request.Form["name"];
            string _email = Request.Form["email"];
            string _password = Request.Form["password"];
            //update ban ghi
            var record = db.users.Where(tbl => tbl.id == _id).FirstOrDefault();
            record.name = _name;
            record.email = _email;
            //neu password khong bằng rỗng thi update password
            if (!string.IsNullOrEmpty(_password))
            {
                //mã hóa password
                _password = getHash(_password);
                record.password = _password;
            }
            db.SaveChanges();
            //======
            return RedirectToAction("list", "Users");
        }
        //form thêm mới
        public IActionResult Add()
        {
            //khai bao bien de chi action cua form
            ViewBag.FormAction = "/Admin/Users/AddPost";
            return View("AddEditUser");
        }
        //khi an nut submit de submit ban ghi
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddPost()
        {
            //lay cac bien form control
            string _name = Request.Form["name"];
            string _email = Request.Form["email"];
            string _password = Request.Form["password"];
            //ma hoa password
            _password = getHash(_password);
            //khoi tao ban ghi
            var record = new users();
            record.name = _name;
            record.email = _email;
            record.password = _password;
            db.users.Add(record);
            db.SaveChanges();
            //------------
            return View("AddEditUser");
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
        public IActionResult Delete()
        {
            int _id = int.Parse(Request.Query["id"]);
            var record = db.users.Where(tbl => tbl.id == _id).FirstOrDefault();
            db.Remove(record);
            db.SaveChanges();
            return RedirectToAction("List", "Users");
        }
        public JsonResult editUser(int id)
        {

            return Json("");
        }
    }
}