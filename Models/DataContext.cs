using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using BTVN11.Models;
namespace BTVN11.Models
{
    public class DataContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-EFJ3SED\\SQLEXPRESS;Database=BTVN;UID=sa;Password=4910587");
        }
        public DbSet<users> users { get; set; }
        public DbSet<EmployeeModel> employeeModel { get; set; }
        public DbSet<CartItem> cartItem { get; set; }
        //public DbSet<categories> categories { get; set; }
    }
}
