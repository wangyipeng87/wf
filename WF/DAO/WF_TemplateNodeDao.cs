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
    public class WF_TemplateNodeDao
    {
        public bool save(WF_TemplateNode entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                conn.Insert<WF_TemplateNode>(entity);
                return true;
            }
        }
        public bool update(WF_TemplateNode entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Update<WF_TemplateNode>(entity);
            }
        }
        public bool del(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                WF_TemplateNode Role = conn.Get<WF_TemplateNode>(id);
                Role.IsDelete = 1;
                return conn.Update<WF_TemplateNode>(Role);
            }
        }
        public int DelByTmpKey(string tmpKey)
        {
            string sql = @"    DELETE FROM WF_TemplateNode WHERE Tmpkey=@tmpkey";
             
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Execute(sql, new { tmpkey = tmpKey });
            }
        }
        public List<WF_TemplateNode> getAllByTmpKey(string tmpkey)
        {
            string sql = @"  SELECT
                                	wtn.ID,
                                	wtn.Tmpkey,
                                	wtn.Nodekey,
                                	wtn.NodeName,
                                	wtn.[Description],
                                	wtn.ProcessType,
                                	wtn.ProcessTypeValue,
                                	wtn.ExecType,
                                	wtn.TimeLimit,
                                	wtn.NodeType,
                                	wtn.[URL],
                                	wtn.IsGoBack,
                                	wtn.GoBackType,
                                	wtn.CreateUserCode,
                                	wtn.CreateTime,
                                	wtn.UpdateUserCode,
                                	wtn.UpdateTime,
                                	wtn.[State],
                                	wtn.IsDelete,
                                	wtn.x,
                                	wtn.y
                                FROM
                                	WF_TemplateNode AS wtn
                                WHERE wtn.Tmpkey=@tmpkey and wtn.[State]=1 and wtn.IsDelete=0";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<WF_TemplateNode>(sql, new { tmpkey = tmpkey }).ToList();
            }
        }
        public WF_TemplateNode getByNodeKey(string tmpkey,string nodeKey)
        {
            string sql = @"  SELECT
                                	wtn.ID,
                                	wtn.Tmpkey,
                                	wtn.Nodekey,
                                	wtn.NodeName,
                                	wtn.[Description],
                                	wtn.ProcessType,
                                	wtn.ProcessTypeValue,
                                	wtn.ExecType,
                                	wtn.TimeLimit,
                                	wtn.NodeType,
                                	wtn.[URL],
                                	wtn.IsGoBack,
                                	wtn.GoBackType,
                                	wtn.CreateUserCode,
                                	wtn.CreateTime,
                                	wtn.UpdateUserCode,
                                	wtn.UpdateTime,
                                	wtn.[State],
                                	wtn.IsDelete,
                                	wtn.x,
                                	wtn.y
                                FROM
                                	WF_TemplateNode AS wtn
                                WHERE wtn.Tmpkey=@tmpkey and wtn.Nodekey=@nodekey and wtn.[State]=1 and wtn.IsDelete=0";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<WF_TemplateNode>(sql, new { tmpkey = tmpkey, nodekey= nodeKey }).FirstOrDefault();
            }
        }
    }
}


