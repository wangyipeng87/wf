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
        public string TmpName { get; set; }
        public string ApplyUserName { get; set; }
        public string WriterUserName { get; set; }
        /// <summary>
        /// 当前审批人
        /// </summary>
        public string UserList { get; set; }
        /// <summary>
        /// 当前审批节点
        /// </summary>
        public string NodeList { get; set; }
        public string StateName { get; set; }
    }
    public class WF_InstanceMap : ClassMapper<WF_Instance>
    {
        public WF_InstanceMap()
        {
            base.Table("WF_Instance");
            this.Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)    
            this.Map(f => f.index).Ignore();
            this.Map(f => f.TmpName).Ignore();
            this.Map(f => f.ApplyUserName).Ignore();
            this.Map(f => f.WriterUserName).Ignore();
            this.Map(f => f.UserList).Ignore();
            this.Map(f => f.NodeList).Ignore();
            this.Map(f => f.StateName).Ignore();
            AutoMap();
        }
    }
}


