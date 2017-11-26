using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Entity
{
    public class WF_ToDo
    {
        public int ID { get; set; }
        public string Nodekey {get; set;}
        public int InstanceID {get; set;}
        public string ToDoName {get; set;}
        public string URL {get; set;}
        public string ResponseUserCode {get; set; }
        public string ResponseUserName { get; set; }
        
        public string DealUserCode {get; set;}
        public DateTime? DealTime {get; set;}
        public int? OperationType {get; set;}
        public int TodoType {get; set;}
        public int IsShow {get; set;}
        public int PrevID {get; set;}
        public int Batch {get; set;}
        public string CreateUserCode {get; set;}
        public DateTime CreateTime {get; set;}
        public string UpdateUserCode {get; set; }
        public string NodeName { get; set; }
        
        public DateTime UpdateTime {get; set;}
        public int State {get; set;}
        public int IsDelete {get; set; }
        public int index { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string FormID { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime? ApplyTime { get; set; }
        /// <summary>
        /// 申请人工号
        /// </summary>
        public string ApplyUserCode { get; set; }
        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplyUserName { get; set; }
        /// <summary>
        /// 填表人工号
        /// </summary>
        public string WriterUserCode { get; set; }
        /// <summary>
        /// 填表人姓名
        /// </summary>
        public string WriterUserName { get; set; }
    }
    public class WF_ToDoMap : ClassMapper<WF_ToDo>
    {
        public WF_ToDoMap()
        {
            base.Table("WF_ToDo");
            this.Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)   
            this.Map(f => f.index).Ignore();
            this.Map(f => f.FormID).Ignore();
            this.Map(f => f.ApplyTime).Ignore();
            this.Map(f => f.ApplyUserCode).Ignore();
            this.Map(f => f.ApplyUserName).Ignore();
            this.Map(f => f.NodeName).Ignore();
            this.Map(f => f.WriterUserCode).Ignore();
            this.Map(f => f.ResponseUserName).Ignore();
            this.Map(f => f.WriterUserName).Ignore();
            AutoMap();
        }
    }
}


