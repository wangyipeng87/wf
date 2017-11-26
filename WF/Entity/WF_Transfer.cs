using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Entity
{
    /// <summary>
    /// 转签历史记录
    /// </summary>
    public class WF_Transfer
    {
        public int ID { get; set; }
        public int beforeToDoID {get; set;}
        public int AfterToDoID {get; set;}
        public string OperationUserCode {get; set;}
        public string OperationUserName {get; set;}
        public string AgentUserCode {get; set;}
        public string AgentUserName {get; set;}
        public DateTime OperationTime {get; set;}
        public int State {get; set;}
        public int IsDelete {get; set;}
    }
    [Serializable]
    public class WF_TransferMap : ClassMapper<WF_Transfer>
    {
        public WF_TransferMap()
        {
            base.Table("WF_Transfer");
            this.Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)   
            AutoMap();
        }
    }
}


