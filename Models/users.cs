using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BTVN11.Models
{
    public class users
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public void AddUsers( users user)
        {
                    
        }
    }
  
}
