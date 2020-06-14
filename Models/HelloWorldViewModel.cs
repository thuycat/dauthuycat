using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTVN11.Models
{
    public class HelloWorldViewModel
    {
        public string ButtonLink { get; set; }
        public HelloWorldViewModel(string buttonLink)
        {
            ButtonLink = buttonLink;
        } 

    }
}
