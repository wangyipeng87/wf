using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Entity
{
    public class WF_InstanceVariable
    {
        public int ID { get; set; }
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

    public class WF_InstanceVariableMap : ClassMapper<WF_InstanceVariable>
    {
        public WF_InstanceVariableMap()
        {
            base.Table("WF_InstanceVariable");
            this.Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)  
            AutoMap();
        }
    }
}


