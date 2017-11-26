using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Entity
{
    public class WF_Template
    {
        public int ID { get; set; }
        public string key {get; set;}
        public string TmpName {get; set;}
        public string Description {get; set;}
        public string CreateUserCode {get; set; }
        public string ClassName { get; set; }
        
        public DateTime CreateTime {get; set;}
        public string UpdateUserCode {get; set;}
        public DateTime UpdateTime {get; set;}
        public int State {get; set;}
        public int IsDelete {get; set; }
        public int index { get; set; }
        public string createuser { get; set; }
        public string updateuser { get; set; }
    }

    [Serializable]
    public class WF_TemplateMap : ClassMapper<WF_Template>
    {
        public WF_TemplateMap()
        {
            base.Table("WF_Template");
            this.Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)    
            this.Map(f => f.index).Ignore();
            this.Map(f => f.createuser).Ignore();
            this.Map(f => f.updateuser).Ignore();
            AutoMap();
        }
    }
}


