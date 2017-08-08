using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Entity
{
    public class WF_Instance
    {
        public string TmpKey {get; set;}
        public string FormID {get; set;}
        public string ApplyUserCode {get; set;}
        public string WriterUserCode {get; set;}
        public string CreateUserCode {get; set;}
        public DateTime CreateTime {get; set;}
        public string UpdateUserCode {get; set;}
        public DateTime UpdateTime {get; set;}
        public int State {get; set;}
        public int IsDelete {get; set;}
    }
}


