using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Entity
{
    public class WF_OperationHistory
    {
        public int ID { get; set; }
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

    public class WF_OperationHistoryMap : ClassMapper<WF_OperationHistory>
    {
        public WF_OperationHistoryMap()
        {
            base.Table("WF_OperationHistory");
            this.Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)   
            AutoMap();
        }
    }
}


