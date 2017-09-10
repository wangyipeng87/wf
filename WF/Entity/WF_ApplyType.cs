using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Entity
{
    public class WF_ApplyType
    {
        public int ID { get; set; }
        public int State { get; set; }
        public string Code { get; set; }
        public string ApplyName { get; set; }
        public string ClassName { get; set; }
        public string CreateUserCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string UpdateUserCode { get; set; }
        public DateTime UpdateTime { get; set; }
        public int IsDelete { get; set; }
        public int index { get; set; }
        public string createuser { get; set; }
        public string updateuser { get; set; }
    }

    [Serializable]
    public class WF_ApplyTypeMap : ClassMapper<WF_ApplyType>
    {
        public WF_ApplyTypeMap()
        {
            base.Table("WF_ApplyType");
            this.Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)  
            this.Map(f => f.index).Ignore();
            this.Map(f => f.createuser).Ignore();
            this.Map(f => f.updateuser).Ignore();
            AutoMap();
        }
    }
}
