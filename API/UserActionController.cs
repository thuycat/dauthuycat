using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BTVN11.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BTVN11.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActionController : ControllerBase
    {
        DataContext db = new DataContext();
        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] users User)
        {
          
            db.users.Add(new users
            {
                name = User.name,
                email= User.email,
                password=  getHash(User.password)
            });
            return Ok(await db.SaveChangesAsync());
        }
        [HttpGet]
        [Route("getID/{id}")]
        public async Task<IActionResult> getID(int id)
        {
            return Ok(await db.users.Where(x => x.id == id).Select(x => new users
            {
                id = x.id,
                name = x.name,
                email = x.email, 
                password = x.password
            }).FirstOrDefaultAsync());
        }
        [HttpPost]
        [Route("EditUser")]
        public async Task<IActionResult> EditUser([FromBody] users user)
        {
            users us = await db.users.Where(x => x.id == user.id).FirstOrDefaultAsync();
            us.name = user.name;
            us.email = user.email;
            if (user.password != null) { 
            us.password = getHash(user.password);
            }
            return Ok(await db.SaveChangesAsync());
        }
        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            users us = await db.users.Where(x => x.id == id).FirstOrDefaultAsync();
            db.Remove(us);          
            return Ok(await db.SaveChangesAsync());
        }
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