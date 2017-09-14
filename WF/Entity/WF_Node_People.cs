using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Entity
{
  public  class WF_Node_People
    {
        public int ID { get; set; }
        public string NodeKey { get; set; }
        public string Tmpkey { get; set; }
        public string UserName { get; set; }
        public string UserCode { get; set; }
        public string CreateUserCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string UpdateUserCode { get; set; }
        public DateTime UpdateTime { get; set; }
        public int State { get; set; }
        public int IsDelete { get; set; }
    }
    [Serializable]
    public class WF_Node_PeopleMap : ClassMapper<WF_Node_People>
    {
        public WF_Node_PeopleMap()
        {
            base.Table("WF_Node_People");
            this.Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)    
            AutoMap();
        }
    }
}
