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
    public class WF_Node_PeopleDao
    {

        public bool save(WF_Node_People entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                conn.Insert<WF_Node_People>(entity);
                return true;
            }
        }
     
        public int del(string tmpkey, string nodekey)
        {
            string sql = @"    DELETE FROM  WF_Node_People   WHERE  NodeKey=@nodekey AND  Tmpkey=@tmpkey ";

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Execute(sql, new { tmpkey = tmpkey, nodekey = nodekey });
            }
        }
      
        public List<WF_Node_People> getAllByNode(string tmpkey,string nodekey)
        {
            string sql = @"  SELECT
 	wnp.ID,
 	wnp.NodeKey,
 	wnp.Tmpkey,
 	wnp.UserName,
 	wnp.UserCode,
 	wnp.CreateUserCode,
 	wnp.CreateTime,
 	wnp.UpdateUserCode,
 	wnp.UpdateTime,
 	wnp.[State],
 	wnp.IsDelete
 FROM
 	WF_Node_People AS wnp WHERE wnp.NodeKey=@nodekey AND wnp.Tmpkey=@tmpkey
 	AND wnp.[State]=1 AND wnp.IsDelete=0 ";

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<WF_Node_People>(sql, new { tmpkey = tmpkey, nodekey = nodekey }).ToList();
            }
        }
      
    }
}
