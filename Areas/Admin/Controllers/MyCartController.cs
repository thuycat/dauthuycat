using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BTVN11.Models;
using Microsoft.AspNetCore.Http;

namespace BTVN11.Areas.Admin.Controllers
{
    public class MyCartController : Controller
    {
        public IActionResult Index()
           
        {                 
            return View();
        }
    }
}