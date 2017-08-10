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
                                     CONVERT(NVARCHAR(4000),wm.[Order])   AS [myindex]
                                FROM   WF_Menu AS wm
                                WHERE  wm.ParenrID = @id
                                		AND wm.[State]=1
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
                                      t.[myindex]+''+CONVERT(NVARCHAR(4000),wm.[Order])  AS [myindex] FROM WF_Menu AS wm
                                       INNER JOIN tmp AS t ON (t.ID=wm.ParenrID AND wm.[State]=1)
                                )
                                
                                SELECT *, ROW_NUMBER() OVER ( ORDER BY  t.[myindex] ASC ) AS [index] FROM tmp AS t ORDER BY t.[myindex]";

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<WF_Menu>(sql, new { id = rootid }).ToList();
            }
        }

    }
}


