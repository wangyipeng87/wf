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
            string sql = @";WITH tmp
                    AS
                    (
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
                    	WF_Menu AS wm WHERE wm.[State]=1 AND wm.ID=@id
                    UNION ALL 
                    
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
                    	wm.[Order] FROM WF_Menu AS wm 
                    INNER JOIN tmp AS t ON t.id=wm.ParenrID
                    WHERE wm.[State]=1
                    )
                    
                    SELECT * FROM tmp AS t";

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<WF_Menu>(sql, new { id = rootid }).ToList();
            }
        }

    }
}


