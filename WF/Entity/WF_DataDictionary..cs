using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF
{
    public class WF_DataDictionary
    {
        public int Type {get; set;}
        public string TypeName {get; set;}
        public int EnumID {get; set;}
        public string EnumName {get; set;}
        public string CreateUserCode {get; set;}
        public DateTime CreateTime {get; set;}
        public string UpdateUserCode {get; set;}
        public DateTime UpdateTime {get; set;}
        public int State {get; set;}
        public int IsDelete {get; set;}
    }
}


