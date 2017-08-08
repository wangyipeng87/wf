using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Entity
{
    public class Department
    {
        public string DeptCode {get; set;}
        public string DeptName {get; set;}
        public string ParentCode {get; set;}
        public string ParentName {get; set;}
        public string AllParentCode {get; set;}
        public string CreateUserCode {get; set;}
        public DateTime CreateTime {get; set;}
        public string UpdateUserCode {get; set;}
        public DateTime UpdateTime {get; set;}
        public int State {get; set;}
        public int IsDelete {get; set;}
    }
}


