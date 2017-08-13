using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WF.Common;
using WF.Entity;

namespace WF.DAO
{
    public class DepartmentDao
    {
        public List<Department> getAll(string rootdeptcode)
        {
            string sql = @"     ;WITH tmp 
                            AS (
                            SELECT
                            	d.ID,
                            	d.DeptCode,
                            	d.DeptName,
                            	d.ParentCode,
                            	d.ParentName,
                            	d.AllParentCode,
                            	d.CreateUserCode,
                            	d.CreateTime,
                            	d.UpdateUserCode,
                            	d.UpdateTime,
                            	d.[State],
                            	d.IsDelete
                            FROM
                            	Department AS d WITH (NOLOCK)
                            WHERE d.DeptCode=@deptcode
                            AND d.[State]=1 AND d.IsDelete=0
                            UNION ALL 
                            SELECT 
                            	d.ID,
                            	d.DeptCode,
                            	d.DeptName,
                            	d.ParentCode,
                            	d.ParentName,
                            	d.AllParentCode,
                            	d.CreateUserCode,
                            	d.CreateTime,
                            	d.UpdateUserCode,
                            	d.UpdateTime,
                            	d.[State],
                            	d.IsDelete FROM Department AS d WITH (NOLOCK)
                            INNER JOIN tmp AS t ON t.DeptCode=d.ParentCode
                            WHERE d.[State]=1 AND d.IsDelete=0
                            )
                            
                            SELECT * FROM tmp AS t";

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["wfdb"].ToString()))
            {
                conn.Open();
                return conn.Query<Department>(sql, new { deptcode = rootdeptcode }).ToList();
            }
        }
    }
}


