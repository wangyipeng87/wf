using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF
{
    public class WF_InstanceVariable
    {
        public int InstanceID {get; set;}
        public string VarName {get; set;}
        public string DefaultValue {get; set;}
        public int VarType {get; set;}
        public string CreateUserCode {get; set;}
        public DateTime CreateTime {get; set;}
        public string UpdateUserCode {get; set;}
        public DateTime UpdateTime {get; set;}
        public int State {get; set;}
        public int IsDelete {get; set;}
    }
}


