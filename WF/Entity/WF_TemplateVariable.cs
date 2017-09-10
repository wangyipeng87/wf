using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Entity
{
    public class WF_TemplateVariable
    {
        public int ID { get; set; }
        public string TmpKey {get; set;}
        public string VarName {get; set;}
        public string DefaultValue {get; set;}
        public int VarType {get; set;}
        public string CreateUserCode {get; set;}
        public DateTime CreateTime {get; set;}
        public string UpdateUserCode {get; set;}
        public DateTime UpdateTime {get; set;}
        public int State {get; set;}
        public int IsDelete {get; set;}

        public string TmpName { get; set; }

        public int index { get; set; }
    }
    [Serializable]
    public class WF_TemplateVariableMap : ClassMapper<WF_TemplateVariable>
    {
        public WF_TemplateVariableMap()
        {
            base.Table("WF_TemplateVariable");
            this.Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)    
            this.Map(f => f.index).Ignore();
            this.Map(f => f.TmpName).Ignore();
            AutoMap();
        }
    }
}


