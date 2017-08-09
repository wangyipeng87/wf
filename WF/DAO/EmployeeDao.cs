using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.Entity;
using Dapper;
using DapperExtensions;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace WF.DAO
{
    public class EmployeeDao
    {
        public Employee getbyAccount(string account)
        {
            string sql = @"  SELECT
 	e.ID,
 	e.UserCode,
 	e.UserName,
 	e.Sex,
 	e.Email,
 	e.PostCode,
 	e.PostName,
 	e.DepCode,
 	e.DeptName,
 	e.Phone,
 	e.LineManageCode,
 	e.CreateUserCode,
 	e.CreateTime,
 	e.UpdateUserCode,
 	e.UpdateTime,
 	e.[State],
 	e.IsDelete,
 	e.[PassWord],
 	e.Account
 FROM
 	Employee AS e WHERE e.Account=@Account";

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<Employee>(sql, new { Account = account }).FirstOrDefault();
            }
        }
        public Employee getbyAccountAndPwd(string account,string pwd)
        {
            string sql = @"   SELECT
 	e.ID,
 	e.UserCode,
 	e.UserName,
 	e.Sex,
 	e.Email,
 	e.PostCode,
 	e.PostName,
 	e.DepCode,
 	e.DeptName,
 	e.Phone,
 	e.LineManageCode,
 	e.CreateUserCode,
 	e.CreateTime,
 	e.UpdateUserCode,
 	e.UpdateTime,
 	e.[State],
 	e.IsDelete,
 	e.[PassWord],
 	e.Account
 FROM
 	Employee AS e WHERE e.Account=@Account AND e.[PassWord]=@pwd";

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<Employee>(sql, new { Account = account, pwd=pwd }).FirstOrDefault();
            }
        }
    }
}


