using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Entity
{
    [Serializable]
    public class Employee
    {
        public int ID { get; set; }
        public string UserCode {get; set; }
        public string Account { get; set; }
        
        public string PassWord { get; set; }
        public string UserName {get; set;}
        public int Sex {get; set;}
        public string Email {get; set;}
        public string PostCode {get; set;}
        public string PostName {get; set;}
        public string DepCode {get; set;}
        public string DeptName {get; set;}
        public string Phone {get; set;}
        public string LineManageCode {get; set;}
        public string CreateUserCode {get; set;}
        public DateTime CreateTime {get; set;}
        public string UpdateUserCode {get; set;}
        public DateTime UpdateTime {get; set;}
        public int State {get; set;}
        public int IsDelete {get; set; }
        public int index { get; set; }
        public string LineManage { get; set; }
        

    }
    [Serializable]
    public class EmployeeMap : ClassMapper<Employee>
    {
        public EmployeeMap()
        {
            base.Table("Employee");
            this.Map(f => f.ID).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)    
            this.Map(f => f.index).Ignore();
            this.Map(f => f.LineManage).Ignore();
            AutoMap();
        }
    }
}


