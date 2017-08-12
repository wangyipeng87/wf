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
    public class WF_MenuDao
    {
        public bool save(WF_Menu entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                conn.Insert<WF_Menu>(entity);
                return true;
            }
        }
        public bool update(WF_Menu entity)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Update<WF_Menu>(entity);
            }
        }
        public bool del(string id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Delete<WF_Menu>(conn.Get<WF_Menu>(id));
            }
        }
        public WF_Menu getByID(string id)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
               return  conn.Get<WF_Menu>(id);
            }
        }
        public List<WF_Menu> getAll(string rootid)
        {
            string sql = @"  ;WITH tmp 
                                AS (
                                SELECT wm.ID,
                                       wm.Name,
                                       wm.Code,
                                       wm.[URL],
                                       wm.ParenrID,
                                       wm.SiteCode,
                                       wm.[State],
                                       wm.CreateUserCode,
                                       wm.CreateTime,
                                       wm.UpdateUserCode,
                                       wm.UpdateTime,
                                       wm.[Order],
                                       1 as [level] ,
                                     CONVERT(NVARCHAR(4000),wm.[Order])   AS [myindex]
                                FROM   WF_Menu AS wm
                                WHERE  wm.ParenrID = @id
                                 UNION ALL
                                       SELECT wm.ID,
                                       wm.Name,
                                       wm.Code,
                                       wm.[URL],
                                       wm.ParenrID,
                                       wm.SiteCode,
                                       wm.[State],
                                       wm.CreateUserCode,
                                       wm.CreateTime,
                                       wm.UpdateUserCode,
                                       wm.UpdateTime,
                                       wm.[Order],  
                                       t.[level]+1 as [level] ,
                                      t.[myindex]+''+CONVERT(NVARCHAR(4000),wm.[Order])  AS [myindex] FROM WF_Menu AS wm
                                       INNER JOIN tmp AS t ON (t.ID=wm.ParenrID )
                                )
                                
                                SELECT *, ROW_NUMBER() OVER ( ORDER BY  t.[myindex] ASC ) AS [index] FROM tmp AS t ORDER BY t.[myindex]";

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<WF_Menu>(sql, new { id = rootid }).ToList();
            }
        }
        public List<WF_Menu> getMenuByParentidAndState(string parentid, int state)
        {
            string sql = @"   
                    SELECT
                    	wm.ID,
                    	wm.Name,
                    	wm.Code,
                    	wm.[URL],
                    	wm.ParenrID,
                    	wm.SiteCode,
                    	wm.[State],
                    	wm.CreateUserCode,
                    	wm.CreateTime,
                    	wm.UpdateUserCode,
                    	wm.UpdateTime,
                    	wm.[Order]
                    FROM
                    	WF_Menu AS wm
                    WHERE wm.ParenrID=@parentid AND (wm.[State]=@state or @state=-1) ORDER BY wm.[Order] ASC ";

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<WF_Menu>(sql, new { parentid = parentid, state = state }).ToList();
            }
        }

    }
}


