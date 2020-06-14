using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTVN11.Models
{
    public class CartItem
    {
        public string img { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int price { get; set; }
        public int amount { get; set; }
        public int money { get { return amount * price; } }
    }
}
