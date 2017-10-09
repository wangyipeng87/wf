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
    public class WF_AgentDao
    {

        public bool save(WF_Agent entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                conn.Insert<WF_Agent>(entity);
                return true;
            }
        }
        public bool update(WF_Agent entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Update<WF_Agent>(entity);
            }
        }
        public bool del(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                WF_Agent agent = conn.Get<WF_Agent>(id);
                agent.IsDelete = 1;
                return conn.Update<WF_Agent>(agent);
            }
        }
        public WF_Agent getByID(int id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Get<WF_Agent>(id);
            }
        }

        public List<WF_Agent> getAll(string origina, string user, int state, int begin, int end, out int count)
        {
            string sql = @"  ;WITH tmp 
                                        AS (
                                        	
                                        SELECT
                                        	wa.ID,
                                        	wa.AgentUserCode,
                                        	wa.AgentName,
                                        	wa.OriginalUserCode,
                                        	wa.OriginalUserName,
                                        	wa.BeginTime,
                                        	wa.EndTime,
                                        	wa.[State],
                                        	wa.CreateUserCode,
                                        	wa.CreateTime,
                                        	wa.UpdateUserCode,
                                        	wa.UpdateTime,
                                        	wa.IsDelete,
                                        	ROW_NUMBER () OVER (ORDER BY wa.CreateTime DESC  ) AS [index]
                                        FROM
                                        	WF_Agent AS wa
                                        WHERE (wa.OriginalUserCode LIKE '%'+@origina+'%' OR wa.OriginalUserName LIKE '%'+@origina+'%'   )
                                        AND (wa.AgentUserCode LIKE '%'+@user+'%' OR wa.AgentName LIKE '%'+@user+'%'   )
                                        AND (@state=-1 OR wa.[State]= @state)
                                        AND wa.IsDelete=0
                                        ) 
                                        SELECT * FROM tmp AS t WHERE t.[index] BETWEEN @begin AND @end";
            string sqlcount = @"   SELECT COUNT(1)
                                            FROM
                                            	WF_Agent AS wa
                                               WHERE (wa.OriginalUserCode LIKE '%'+@origina+'%' OR wa.OriginalUserName LIKE '%'+@origina+'%'   )
                                        AND (wa.AgentUserCode LIKE '%'+@user+'%' OR wa.AgentName LIKE '%'+@user+'%'   )
                                            AND (@state=-1 OR wa.[State]= @state)
                                            AND wa.IsDelete=0 ";
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                count = conn.Query<int>(sqlcount, new { origina = origina, user = user, state = state }).FirstOrDefault();
                return conn.Query<WF_Agent>(sql, new { origina = origina, user = user, state = state, begin = begin, end = end }).ToList();
            }
        }
        public List<WF_Agent> getAgentByOrg(string orgusercode)
        {
            string sql = @"    SELECT
                             	wa.ID,
                             	wa.AgentUserCode,
                             	wa.AgentName,
                             	wa.OriginalUserCode,
                             	wa.OriginalUserName,
                             	wa.BeginTime,
                             	wa.EndTime,
                             	wa.[State],
                             	wa.CreateUserCode,
                             	wa.CreateTime,
                             	wa.UpdateUserCode,
                             	wa.UpdateTime,
                             	wa.IsDelete
                             FROM
                             	WF_Agent AS wa
                             WHERE wa.OriginalUserCode=@orgusercode AND wa.IsDelete=0
                             AND wa.BeginTime<=GETDATE() AND wa.EndTime>=GETDATE()";
            
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open(); 
                return conn.Query<WF_Agent>(sql, new { orgusercode = orgusercode }).ToList();
            }
        }
    }
}


