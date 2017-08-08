using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Entity
{
    public class WF_Agent
    {
        public string AgentUserCode {get; set;}
        public string AgentName {get; set;}
        public string OriginalUserCode {get; set;}
        public string OriginalUserName {get; set;}
        public DateTime BeginTime {get; set;}
        public DateTime EndTime {get; set;}
        public int State {get; set;}
        public string CreateUserCode {get; set;}
        public DateTime CreateTime {get; set;}
        public string UpdateUserCode {get; set;}
        public DateTime UpdateTime {get; set;}
        public int IsDelete {get; set;}
    }
}


