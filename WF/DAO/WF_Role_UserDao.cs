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
    public class WF_Role_UserDao
    {
        public bool save(WF_Role_User entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                conn.Insert<WF_Role_User>(entity);
                return true;
            }
        }
        public bool update(WF_Role_User entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Update<WF_Role_User>(entity);
            }
        }
        public bool del(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                WF_Role_User Role_User = conn.Get<WF_Role_User>(id);
                Role_User.IsDelete = 1;
                return conn.Update<WF_Role_User>(Role_User);
            }
        }
        public WF_Role_User getByID(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Get<WF_Role_User>(id);
            }
        }
        public WF_Role_User getRoleUserByID(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                string sql = @"  SELECT
                                     	wru.ID,
                                     	wr.RoleName,
                                     	wru.RoleCode,
                                     	e.UserName,
                                     	e.UserCode,
                                     	wru.[State]
                                     FROM
                                     	WF_Role_User AS wru
                                     	INNER JOIN WF_Role AS wr ON wru.RoleCode=wr.RoleCode
                                     	INNER JOIN Employee AS e ON wru.UserCode=e.UserCode
                        WHERE wru.ID=@id";
                return conn.Query<WF_Role_User>(sql, new { id = id }).FirstOrDefault(); ;
            }
        }
        public List<WF_Role_User> getAll(string key, string rolecode, int state, int begin, int end,  out int count)
        {
            string sql = @"    ;WITH tmp
                                     AS(
                                     SELECT
                                     	wru.ID,
                                     	wr.RoleName,
                                     	wru.RoleCode,
                                     	e.UserName,
                                     	e.UserCode,
                                     	wru.[State],
                                     	ROW_NUMBER() OVER ( ORDER BY e.UserName DESC ) AS [index]
                                     FROM
                                     	WF_Role_User AS wru
                                     	INNER JOIN WF_Role AS wr ON wru.RoleCode=wr.RoleCode
                                     	INNER JOIN Employee AS e ON wru.UserCode=e.UserCode
                                     WHERE wru.IsDelete=0
                                     AND wr.IsDelete=0
                                     AND e.IsDelete=0
                                     and wr.rolecode=@rolecode
                                    and (
                                                        @state=-1 
                                                            OR 
                                                       wru.[State] =@state
                                                   )
                                     and  (e.UserName LIKE '%'+@key+'%' OR e.UserCode LIKE '%'+@key+'%' OR e.Account LIKE '%'+@key+'%' )
                                     )
                                     SELECT * FROM tmp AS t WHERE t.[index] BETWEEN @begin AND @end";
            string sqlcount = @"   SELECT COUNT(1)
                                        FROM   WF_Role_User         AS wru
                                               INNER JOIN WF_Role   AS wr
                                                    ON  wru.RoleCode = wr.RoleCode
                                               INNER JOIN Employee  AS e
                                                    ON  wru.UserCode = e.UserCode
                                        WHERE  wru.IsDelete = 0
                                               AND wr.IsDelete = 0
                                               AND e.IsDelete = 0
                                               and wr.rolecode=@rolecode
                                               and (
                                                        @state=-1 
                                                            OR 
                                                       wru.[State] =@state
                                                   )
                                               and (
                                                       e.UserName LIKE '%' + @key + '%'
                                                       OR e.UserCode LIKE '%' + @key + '%'
                                                       OR e.Account LIKE '%' + @key + '%'
                                                   )";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                count = conn.Query<int>(sqlcount, new {  key = key, rolecode=rolecode, state= state }).FirstOrDefault();
                return conn.Query<WF_Role_User>(sql, new {  key = key, rolecode = rolecode, state= state, begin = begin, end = end }).ToList();
            }
        }

        public List<WF_Role_User>  getRoleUserByRoleCode(string code)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                string sql = @"   
                                SELECT wru.ID,
                                       wr.RoleName,
                                       wru.RoleCode,
                                       e.UserName,
                                       e.UserCode,
                                       wru.[State]
                                FROM   WF_Role_User AS wru
                                INNER JOIN WF_Role AS wr ON wr.RoleCode=wru.RoleCode
                                INNER JOIN Employee AS e ON e.UserCode=wru.UserCode
                                WHERE  wru.[State] = 1
                                AND wr.[State]=1 AND e.[State]=1
                                       AND wru.RoleCode = @code";
                return conn.Query<WF_Role_User>(sql, new { code = code }).ToList(); ;
            }
        }
    }
}


