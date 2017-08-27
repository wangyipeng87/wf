using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.Common;
using WF.Entity;
using Dapper;
using DapperExtensions;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WF.DAO
{
    public class WF_TemplateDao
    {

        public bool save(WF_Template entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                conn.Insert<WF_Template>(entity);
                return true;
            }
        }
        public bool update(WF_Template entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Update<WF_Template>(entity);
            }
        }
        public bool del(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                WF_Template Role = conn.Get<WF_Template>(id);
                Role.IsDelete = 1;
                return conn.Update<WF_Template>(Role);
            }
        }
        public WF_Template getByID(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Get<WF_Template>(id);
            }
        }
        public WF_Template getByKey(string key)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                string sql = @"SELECT
                                	wt.ID,
                                	wt.[key],
                                	wt.TmpName,
                                	wt.[Description],
                                	wt.CreateUserCode,
                                	wt.CreateTime,
                                	wt.UpdateUserCode,
                                	wt.UpdateTime,
                                	wt.[State],
                                	wt.IsDelete
                                FROM
                                	WF_Template AS wt
                                WHERE wt.[key]=@key";
                return conn.Query<WF_Template>(sql, new { key = key }).FirstOrDefault();
            }
        }
        public List<WF_Template> getAll(string key, int state, int begin, int end, string order, out int count)
        {
            string sql = @"   ;WITH tmp 
                                     AS (
                                     SELECT
                                           	wt.ID,
                                           	wt.[key],
                                           	wt.TmpName,
                                           	wt.[Description],
                                           	wt.CreateUserCode,
                                           	wt.CreateTime,
                                           	wt.UpdateUserCode,
                                           	wt.UpdateTime,
                                           	wt.[State],
                                           	wt.IsDelete,
                                           	e.UserName+'('+e.UserCode+')' AS createuser,
                                           	e2.UserName+'('+e2.UserCode+')' AS updateuser,
                                           	ROW_NUMBER() OVER ( ORDER BY "+order+@"  ) AS [index]
                                           FROM
                                           	WF_Template AS wt
                                           	INNER JOIN Employee AS e ON e.UserCode=wt.CreateUserCode
                                           	INNER JOIN Employee AS e2 ON e2.UserCode=wt.UpdateUserCode
                                           WHERE wt.IsDelete=0
                                           AND (wt.[key] LIKE '%'+@key+'%' OR wt.TmpName  LIKE '%'+@key+'%')
                                           AND (@state=-1 OR wt.[State]=@state)
                                     )
                                     
                                     SELECT * FROM tmp AS t where t.[index] BETWEEN @begin AND @end";
            string sqlcount = @"    SELECT  COUNT(1)
                                             FROM
                                             	WF_Template AS wt
                                             WHERE  wt.IsDelete=0
                                           AND (wt.[key] LIKE '%'+@key+'%' OR wt.TmpName  LIKE '%'+@key+'%')
                                           AND (@state=-1 OR wt.[State]=@state)";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                count = conn.Query<int>(sqlcount, new { state = state, key = key }).FirstOrDefault();
                return conn.Query<WF_Template>(sql, new { state = state, key = key, begin = begin, end = end }).ToList();
            }
        }
    }
}


