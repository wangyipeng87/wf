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

        public List<Employee> getAll()
        {
            string sql = @"   SELECT top 10
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
	Employee AS e
WHERE e.[State]=1";

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<Employee>(sql).ToList();
            }
        }
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
        public List<Employee> getEmpForAutocomplete(string key)
        {
            string sql = @"  SELECT TOP 10
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
                                	Employee AS e
                                WHERE e.[State]=1
                                AND e.IsDelete=0
                                AND (e.UserName LIKE '%'+@key+'%' OR e.UserCode LIKE '%'+@key+'%' OR e.Account LIKE '%'+@key+'%')";

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<Employee>(sql, new { key = key }).ToList();
            }
        }
        public List<Employee> getAll(string key, string linemanage, int state,int  begin,int end,string order,out int count)
        {
            string sql = @"     ;WITH tmp 
                                     AS (
                                     
                                     SELECT
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
                                     	e.Account,
 	                                    e2.UserName+'('+e2.UserCode+')' AS LineManage,
                                     	ROW_NUMBER() OVER ( ORDER BY " + order + @" ) AS [index]
                                     	
                                     FROM
                                     	Employee AS e 
                                     LEFT JOIN Employee AS e2 ON e.LineManageCode=e2.UserCode
                                     WHERE (@state =-1 OR  e.[State]=@state )
                                     AND (e.UserName LIKE  '%'+@key+'%' OR  e.UserCode LIKE  '%'+@key+'%' OR  e.Account LIKE  '%'+@key+'%')
                                        AND ( @linemanage  is null or  @linemanage  ='' or e2.UserName   like  '%'+@linemanage+'%' OR  e2.UserCode like  '%'+@linemanage+'%'   OR  e2.Account  like  '%'+@linemanage+'%'   )
               
                                   ) 
                                     SELECT * FROM tmp AS t WHERE t.[index] BETWEEN @begin AND @end";
            string sqlcount = @"     select count(1)
                                     FROM
                                     	Employee AS e 
                                     LEFT JOIN Employee AS e2 ON e.LineManageCode=e2.UserCode
                                     WHERE (@state =-1 OR  e.[State]=@state )
                                     AND (e.UserName LIKE  '%'+@key+'%' OR  e.UserCode LIKE  '%'+@key+'%' OR  e.Account LIKE  '%'+@key+'%')  
                                     AND ( @linemanage  is null or  @linemanage  ='' or e2.UserName   like  '%'+@linemanage+'%'  OR  e2.UserCode like  '%'+@linemanage+'%'   OR  e2.Account  like  '%'+@linemanage+'%'   )
               
                                     ";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                count = conn.Query<int>(sqlcount, new { state = state, key = key, linemanage = linemanage }).FirstOrDefault();
                return conn.Query<Employee>(sql,new { state=state, key= key, begin= begin, end= end,linemanage=linemanage }).ToList();
            }
        }
    }
}


