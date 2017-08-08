using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF
{
    public class T_CMS_Menu
    {
        public string ID {get; set;}
        public string Name {get; set;}
        public string Code {get; set;}
        public string URL {get; set;}
        public string ParenrID {get; set;}
        public string SiteCode {get; set;}
        public int State {get; set;}
        public string CreateUserCode {get; set;}
        public DateTime CreateTime {get; set;}
        public string UpdateUserCode {get; set;}
        public DateTime UpdateTime {get; set;}
        public int Order {get; set;}
    }
}


