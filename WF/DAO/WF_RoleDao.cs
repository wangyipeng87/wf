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
    public class WF_RoleDao
    {
        public bool save(WF_Role entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                conn.Insert<WF_Role>(entity);
                return true;
            }
        }
        public bool update(WF_Role entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Update<WF_Role>(entity);
            }
        }
        public bool del(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                WF_Role Role = conn.Get<WF_Role>(id);
                Role.IsDelete = 1;
                return conn.Update<WF_Role>(Role);
            }
        }
        public WF_Role getByID(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Get<WF_Role>(id);
            }
        }
        public List<WF_Role> getAll(string key,  int state, int begin, int end, string order, out int count)
        {
            string sql = @"   ;WITH tmp 
                                     AS (
                                     SELECT
                                     	wr.ID,
                                     	wr.RoleCode,
                                     	wr.RoleName,
                                     	wr.CreateUserCode,
                                     	wr.CreateTime,
                                     	wr.UpdateUserCode,
                                     	wr.UpdateTime,
                                     	wr.[State],
                                     	wr.IsDelete,
                                     	ROW_NUMBER() OVER ( ORDER BY "+order+ @" ) AS [index]
                                     FROM
                                     	WF_Role AS wr
                                     WHERE wr.IsDelete=0
                                     and (wr.RoleCode LIKE '%'+@key+'%' OR wr.RoleName  LIKE '%'+@key+'%')
                                     AND (@state=-1 OR wr.[State]= @state)
                                     )
                                     
                                     SELECT * FROM tmp AS t where t.[index] BETWEEN @begin AND @end";
            string sqlcount = @"    SELECT  COUNT(1)
                                             FROM
                                             	WF_Role AS wr
                                             WHERE wr.IsDelete=0
                                             and (wr.RoleCode LIKE '%'+@key+'%' OR wr.RoleName  LIKE '%'+@key+'%')
                                             AND (@state=-1 OR wr.[State]= @state) ";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                count = conn.Query<int>(sqlcount, new { state = state, key = key }).FirstOrDefault();
                return conn.Query<WF_Role>(sql, new { state = state, key = key, begin = begin, end = end }).ToList();
            }
        }
    }
}


