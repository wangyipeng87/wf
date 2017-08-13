using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Entity
{
    public class WF_Role_User
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public string RoleCode { get; set; }
        public string UserName { get; set; }
        public string UserCode {get; set;}
        public string CreateUserCode {get; set;}
        public DateTime CreateTime {get; set;}
        public string UpdateUserCode {get; set;}
        public DateTime UpdateTime {get; set;}
        public int State {get; set;}
        public int IsDelete {get; set; }
        public int index { get; set; }
    }

    [Serializable]
    public class WF_Role_UserMap : ClassMapper<WF_Role_User>
    {
        public WF_Role_UserMap()
        {
            base.Table("WF_Role_User");
            this.Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)    
            this.Map(f => f.index).Ignore();
            this.Map(f => f.RoleName).Ignore();
            this.Map(f => f.UserName).Ignore();
            AutoMap();
        }
    }
}


