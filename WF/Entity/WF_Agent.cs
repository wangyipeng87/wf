using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Entity
{
    public class WF_Agent
    {
        public int ID { get; set; }
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
        public int IsDelete {get; set; }
        public int index { get; set; }
    }

    [Serializable]
    public class WF_AgentMap : ClassMapper<WF_Agent>
    {
        public WF_AgentMap()
        {
            base.Table("WF_Agent");
            this.Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)    
            this.Map(f => f.index).Ignore();
            AutoMap();
        }
    }
}


