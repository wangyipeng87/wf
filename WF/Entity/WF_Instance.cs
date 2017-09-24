using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Entity
{
    public class WF_Instance
    {
        public int ID { get; set; }
        public string TmpKey {get; set;}
        public string FormID {get; set;}
        public string ApplyUserCode {get; set;}
        public string WriterUserCode {get; set;}
        public string CreateUserCode {get; set;}
        public DateTime CreateTime {get; set;}
        public string UpdateUserCode {get; set;}
        public DateTime UpdateTime {get; set;}
        public int State {get; set;}
        public int IsDelete {get; set; }
        public int index { get; set; }
    }
    public class WF_InstanceMap : ClassMapper<WF_Instance>
    {
        public WF_InstanceMap()
        {
            base.Table("WF_Instance");
            this.Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)    
            this.Map(f => f.index).Ignore();
            AutoMap();
        }
    }
}


