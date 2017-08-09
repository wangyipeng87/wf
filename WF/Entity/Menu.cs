using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Entity
{
   public class Menu
    {
        public String name { get; set; }
        public String url { get; set; }
        public String code { get; set; }
        public String target { get; set; }
        public String html { get; set; }
        public String classname { get; set; }
   
        public List<Menu> submenu { get; set; }

    }
}
