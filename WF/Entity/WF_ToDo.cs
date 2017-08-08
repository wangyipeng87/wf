using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Entity
{
    public class WF_ToDo
    {
        public string Nodekey {get; set;}
        public int InstanceID {get; set;}
        public string ToDoName {get; set;}
        public string URL {get; set;}
        public string ResponseUserCode {get; set;}
        public string DealUserCode {get; set;}
        public DateTime DealTime {get; set;}
        public int OperationType {get; set;}
        public int TodoType {get; set;}
        public int IsShow {get; set;}
        public int PrevID {get; set;}
        public int Batch {get; set;}
        public string CreateUserCode {get; set;}
        public DateTime CreateTime {get; set;}
        public string UpdateUserCode {get; set;}
        public DateTime UpdateTime {get; set;}
        public int State {get; set;}
        public int IsDelete {get; set;}
    }
}


