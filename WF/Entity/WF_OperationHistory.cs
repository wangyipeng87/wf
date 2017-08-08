using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF
{
    public class WF_OperationHistory
    {
        public int InstanceID {get; set;}
        public int TodoID {get; set;}
        public string OperationUserCode {get; set;}
        public string OperationUserName {get; set;}
        public string AgentUserCode {get; set;}
        public string AgentUserName {get; set;}
        public DateTime OperationTime {get; set;}
        public int OperationType {get; set;}
        public string Comments {get; set;}
        public int State {get; set;}
        public int IsDelete {get; set;}
    }
}


