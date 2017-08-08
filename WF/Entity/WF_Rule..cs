using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF
{
    public class WF_Rule
    {
        public string Tmpkey {get; set;}
        public string Rulekey {get; set;}
        public string BeginNodeKey {get; set;}
        public string EndNodekey {get; set;}
        public string Expression {get; set;}
        public string Description {get; set;}
        public string CreateUserCode {get; set;}
        public DateTime CreateTime {get; set;}
        public string UpdateUserCode {get; set;}
        public DateTime UpdateTime {get; set;}
        public int State {get; set;}
        public int IsDelete {get; set;}
    }
}


