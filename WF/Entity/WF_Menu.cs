using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Entity
{
    public class WF_Menu
    {
        public string ID {get; set;}
        public string Name {get; set;}
        public string Code {get; set;}
        public string URL {get; set;}
        public string ParenrID {get; set;}
        public string SiteCode {get; set;}
        public int State {get; set;}
        public string CreateUserCode {get; set;}
        public DateTime CreateTime {get; set;}
        public string UpdateUserCode {get; set;}
        public DateTime UpdateTime {get; set;}
        public int Order {get; set; }
        public string myindex { get; set; }
        public int index { get; set; }
        public int level { get; set; }
        
    }
    [Serializable]
    public class WF_MenuMap : ClassMapper<WF_Menu>
    {
        public WF_MenuMap()
        {
            base.Table("WF_Menu");
            this.Map(f => f.ID).Key(KeyType.Guid);//设置主键  (如果主键名称不包含字母“ID”，请设置)    
            this.Map(f => f.myindex).Ignore();
            this.Map(f => f.index).Ignore();
            this.Map(f => f.level).Ignore();
            AutoMap();
        }
    }
}


