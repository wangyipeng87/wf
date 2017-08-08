using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Entity
{
    public class WF_TemplateNode
    {
        public string key {get; set;}
        public string Nodekey {get; set;}
        public string NodeName {get; set;}
        public string Description {get; set;}
        public int ProcessType {get; set;}
        public string ProcessTypeValue {get; set;}
        public int ExecType {get; set;}
        public int TimeLimit {get; set;}
        public int NodeType {get; set;}
        public string URL {get; set;}
        public int IsGoBack {get; set;}
        public int GoBackType {get; set;}
        public string CreateUserCode {get; set;}
        public DateTime CreateTime {get; set;}
        public string UpdateUserCode {get; set;}
        public DateTime UpdateTime {get; set;}
        public int State {get; set;}
        public int IsDelete {get; set;}
        public double x {get; set;}
        public double y {get; set;}
    }
}


