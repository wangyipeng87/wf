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
    public class WF_ToDoDao
    {
        public int save(WF_ToDo entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
               object id= conn.Insert<WF_ToDo>(entity);
                return (int)id;
            }
        }
        public bool update(WF_ToDo entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Update<WF_ToDo>(entity);
            }
        }
        public bool del(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                WF_ToDo todo= conn.Get<WF_ToDo>(id);
                todo.IsDelete = 1;
                return conn.Update<WF_ToDo>(todo);
            }
        }
        public WF_ToDo getByID(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Get<WF_ToDo>(id);
            }
        }

        public List<WF_ToDo> getList(int instanceid,string nodekey,int state)
        {
            string sql = @"   SELECT
                                	wtd.ID,
                                	wtd.Nodekey,
                                	wtd.InstanceID,
                                	wtd.ToDoName,
                                	wtd.[URL],
                                	wtd.ResponseUserCode,
                                	wtd.DealUserCode,
                                	wtd.DealTime,
                                	wtd.OperationType,
                                	wtd.TodoType,
                                	wtd.IsShow,
                                	wtd.PrevID,
                                	wtd.Batch,
                                	wtd.CreateUserCode,
                                	wtd.CreateTime,
                                	wtd.UpdateUserCode,
                                	wtd.UpdateTime,
                                	wtd.[State],
                                	wtd.IsDelete
                                FROM
                                	WF_ToDo AS wtd WHERE wtd.InstanceID=@instanceid AND wtd.Nodekey=@nodekey AND wtd.[State]=@state
                                	AND wtd.IsDelete=0";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<WF_ToDo>(sql, new { instanceid = instanceid, nodekey= nodekey, state= state }).ToList();
            }
        }
    }
}


