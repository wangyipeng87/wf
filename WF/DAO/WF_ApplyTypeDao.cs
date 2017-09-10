using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.Entity;

namespace WF.DAO
{
    public class WF_ApplyTypeDao
    {
        public bool save(WF_ApplyType entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                conn.Insert<WF_ApplyType>(entity);
                return true;
            }
        }
        public bool update(WF_ApplyType entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Update<WF_ApplyType>(entity);
            }
        }
        public bool del(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                WF_ApplyType Role = conn.Get<WF_ApplyType>(id);
                Role.IsDelete = 1;
                return conn.Update<WF_ApplyType>(Role);
            }
        }
        public WF_ApplyType getByID(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Get<WF_ApplyType>(id);
            }
        }
        public WF_ApplyType getByCode(string code)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                string sql = @" SELECT
                        	wat.ID,
                        	wat.Code,
                        	wat.ApplyName,
                        	wat.ClassName,
                        	wat.CreateUserCode,
                        	wat.CreateTime,
                        	wat.UpdateUserCode,
                        	wat.UpdateTime,
                        	wat.[State],
                        	wat.IsDelete
                        FROM
                        	WF_ApplyType AS wat WHERE wat.Code=@code";
                return conn.Query<WF_ApplyType>(sql, new { code = code }).FirstOrDefault();
            }
        }

        public List<WF_ApplyType> getAll()
        {
            string sql = @"    SELECT
	wat.ID,
	wat.Code,
	wat.ApplyName,
	wat.ClassName,
	wat.CreateUserCode,
	wat.CreateTime,
	wat.UpdateUserCode,
	wat.UpdateTime,
	wat.[State],
	wat.IsDelete
FROM
	WF_ApplyType AS wat WHERE wat.IsDelete=0 AND wat.[State]=1";

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<WF_ApplyType>(sql).ToList();
            }
        }
        public List<WF_ApplyType> getAll(string key, int state, int begin, int end, string order, out int count)
        {
            string sql = @"   ; WITH tmp 
                                     AS (
                                         SELECT wt.ID,
                                                wt.Code,
                                                wt.ApplyName,
                                                wt.ClassName,
                                                wt.CreateUserCode,
                                                wt.CreateTime,
                                                wt.UpdateUserCode,
                                                wt.UpdateTime,
                                                wt.[State],
                                                wt.IsDelete,
                                                e.UserName + '(' + e.UserCode + ')' AS createuser,
                                                e2.UserName + '(' + e2.UserCode + ')' AS updateuser,
                                                ROW_NUMBER() OVER(ORDER BY " + order + @") AS [index]
                                         FROM   WF_ApplyType         AS wt
                                                INNER JOIN Employee  AS e
                                                     ON  e.UserCode = wt.CreateUserCode
                                                INNER JOIN Employee  AS e2
                                                     ON  e2.UserCode = wt.UpdateUserCode
                                         WHERE  wt.IsDelete = 0
                                                AND (
                                                        wt.[Code] LIKE '%' + @key + '%'
                                                        OR wt.ApplyName LIKE '%' + @key + '%'
                                                    )
                                                AND (@state = -1 OR wt.[State] = @state)
                                     )
                                                                     
                                SELECT *
                                FROM   tmp AS t
                                WHERE  t.[index] BETWEEN @begin AND @end";
            string sqlcount = @"    SELECT  COUNT(1)
                                             FROM
                                             	WF_ApplyType AS wt
                                             WHERE  wt.IsDelete=0
                                           AND (wt.[Code] LIKE '%'+@key+'%' OR wt.ApplyName  LIKE '%'+@key+'%')
                                           AND (@state=-1 OR wt.[State]=@state)";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                count = conn.Query<int>(sqlcount, new { state = state, key = key }).FirstOrDefault();
                return conn.Query<WF_ApplyType>(sql, new { state = state, key = key, begin = begin, end = end }).ToList();
            }
        }
    }
}
