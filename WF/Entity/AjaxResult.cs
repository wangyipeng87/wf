using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Entity
{
    public class AjaxResult
    {
        public string code { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public int totle { get; set; }
    }
    public class ResultCode
    {
        public const string OK = "200";
        public const string ERROR = "500";
    }
}
