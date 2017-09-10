using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WF.Entity;

namespace WF.DAO
{
    public class WF_TemplateVariableDao
    {
        public bool save(WF_TemplateVariable entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                conn.Insert<WF_TemplateVariable>(entity);
                return true;
            }
        }
        public bool update(WF_TemplateVariable entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Update<WF_TemplateVariable>(entity);
            }
        }
        public bool del(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                WF_TemplateVariable Role_User = conn.Get<WF_TemplateVariable>(id);
                Role_User.IsDelete = 1;
                return conn.Update<WF_TemplateVariable>(Role_User);
            }
        }
        public WF_TemplateVariable getByID(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Get<WF_TemplateVariable>(id);
            }
        }
        public WF_TemplateVariable getVarByID(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                string sql = @"                           SELECT
                                 	wru.ID,
                                     	wru.TmpKey,
                                     	wru.VarName,
                                     	wru.DefaultValue,
                                     	wru.VarType,
                                     	wru.CreateUserCode,
                                     	wru.CreateTime,
                                     	wru.UpdateUserCode,
                                     	wru.UpdateTime,
                                     	wru.[State],
                                     	wru.IsDelete,
                                     	wr.TmpName
                                     FROM WF_TemplateVariable AS wru
                                     	INNER JOIN WF_Template  AS wr ON wru.TmpKey=wr.[key]
                                     WHERE wru.ID=@id";
                return conn.Query<WF_TemplateVariable>(sql, new { id = id }).FirstOrDefault(); ;
            }
        }
        public List<WF_TemplateVariable> getAll(string key, string tmpkey, int state, int begin, int end, out int count)
        {
            string sql = @"       ;WITH tmp
                                     AS(
                                     SELECT
                                      	wru.ID,
                                     	wru.TmpKey,
                                     	wru.VarName,
                                     	wru.DefaultValue,
                                     	wru.VarType,
                                     	wru.CreateUserCode,
                                     	wru.CreateTime,
                                     	wru.UpdateUserCode,
                                     	wru.UpdateTime,
                                     	wru.[State],
                                     	wru.IsDelete,
                                     	wr.TmpName,
                                     	ROW_NUMBER() OVER ( ORDER BY  wru.CreateTime DESC ) AS [index]
                                     FROM WF_TemplateVariable AS wru
                                     	INNER JOIN WF_Template  AS wr ON wru.TmpKey=wr.[key]
                                     WHERE wru.IsDelete=0
                                     AND wr.IsDelete=0
                                     and wr.[key]=@tmpkey
                                     and (
                                                        @state=-1 
                                                            OR 
                                                       wru.[State] =@state
                                                   )
                                     and  (wru.VarName LIKE '%'+@key+'%' )
                                     )
                                     SELECT * FROM tmp AS t WHERE t.[index] BETWEEN @begin AND @end
                                     
                                  ";
            string sqlcount = @"       SELECT
                                      COUNT(1)
                                     FROM WF_TemplateVariable AS wru
                                     	INNER JOIN WF_Template  AS wr ON wru.TmpKey=wr.[key]
                                     WHERE wru.IsDelete=0
                                     AND wr.IsDelete=0
                                     and wr.[key]=@tmpkey
                                     and (
                                                        @state=-1 
                                                            OR 
                                                       wru.[State] =@state
                                                   )
                                     and  (wru.VarName LIKE '%'+@key+'%' )
                                  ";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                count = conn.Query<int>(sqlcount, new { key = key, tmpkey = tmpkey, state = state }).FirstOrDefault();
                return conn.Query<WF_TemplateVariable>(sql, new { key = key, tmpkey = tmpkey, state = state, begin = begin, end = end }).ToList();
            }
        }
    }
}


